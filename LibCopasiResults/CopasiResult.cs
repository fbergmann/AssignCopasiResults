using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using org.COPASI;

namespace LibCopasiResults
{
    public class CopasiResult : IDisposable
    {
        private CCopasiDataModel _DataModel;
        public List<FittingItem> FittingItems { get; set; }
        public List<CheckPoint> Data { get; set; }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_DataModel != null)
                {
                    _DataModel.Dispose();
                    _DataModel = null;
                }
        }

        ~CopasiResult()
        {
            Dispose(false);
        }

        public void ExportSBML(string fileName)
        {
            _DataModel.exportSBML(fileName, true, 2, 4);
        }
        public void LoadCopasi(string copasiFile)
        {
            _DataModel.loadModel(copasiFile);
        }

        public void SaveCPS(string fileName)
        {
            _DataModel.saveModel(fileName, true);                
        }

        public static string GetTempFileInPath(string dir)
        {
            int index = 0; 
            var current = Path.Combine(dir, string.Format("temp{0}.cps", index));
            while(File.Exists(current))
            {
                index++;
                current = Path.Combine(dir, string.Format("temp{0}.cps", index));
            }
            return current;
        }
        public string SaveTempIn(string dir)        
        {
            
            CFitTask fitTask = GetFitTask();
            if (fitTask == null)
                return null;

            for (uint i = 0; i < _DataModel.getPlotDefinitionList().size(); i++)
            {
                _DataModel.getPlotSpecification(i).setActive(false);
            }
            
            
            var file = GetTempFileInPath(dir);

            fitTask.setScheduled(true);
            fitTask.getReport().setTarget("");
            CFitProblem fitProblem = (CFitProblem)fitTask.getProblem();
            fitProblem.setCalculateStatistics(false);
            fitProblem.setRandomizeStartValues(false);

            var plot1 = _DataModel.getPlotSpecification(_DataModel.getPlotDefinitionList().size() - 1);
            COutputAssistant.createDefaultOutput(910, fitTask, _DataModel);

            var plot = _DataModel.getPlotSpecification(_DataModel.getPlotDefinitionList().size()-1);


            fitTask.setMethodType(15);
            //fitTask.

            SaveCPS(file);
            return file;
        }

        private bool CheckDataAgainstModelForParameterEstimation()
        {
            var fit = GetFitTask();
            if (fit == null) throw new ArgumentException("The model does not contain a parameter fitting task.");
            CFitProblem problem = (CFitProblem)fit.getProblem();
            problem.setModel(_DataModel.getModel());

            uint taskFitItems = problem.getOptItemSize();
            if (taskFitItems > FittingItems.Count)
                throw new ArgumentException(string.Format("The number of fitting items of the model {0} does not match the number of fitting items in the data file loaded {1}. The data cannot be overwritten. Please be sure to load the correct data file for this model.", taskFitItems, FittingItems.Count));

            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);
                if (FittingItems[i].Name != item.getObjectDisplayName().Replace("\"", ""))
                    throw new ArgumentException(string.Format("The name of item number {0} does not match with the item in the data. Item in model was {1} and the item in the data was {2}.", i, item.getObjectDisplayName(), FittingItems[i].Name));
            }

            return true;
        }

        private bool CheckDataAgainstModelForOptimization()
        {
            var opt = GetOptTask();
            if (opt == null) throw new ArgumentException("The model does not contain an optimization task.");
            var problem = (COptProblem)opt.getProblem();
            problem.setModel(_DataModel.getModel());

            uint taskItems = problem.getOptItemSize();
            if (taskItems > FittingItems.Count)
                throw new ArgumentException(string.Format("The number of optimization items of the model {0} does not match the number of fitting items in the data file loaded {1}. The data cannot be overwritten. Please be sure to load the correct data file for this model.", taskItems, FittingItems.Count));

            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);
                string name = CRUtils.SanitizeName(item.getObjectDisplayName());
                if (FittingItems[i].Name != name)
                    throw new ArgumentException(string.Format("The name of item number {0} does not match with the item in the data. Item in model was {1} and the item in the data was {2}.", i, item.getObjectDisplayName(), FittingItems[i].Name));
            }

            return true;
        }
        public bool CheckDataAgainstModel()
        {
            if (IsOptimization)
            {
                return CheckDataAgainstModelForOptimization();
            }
            else
            {
                return CheckDataAgainstModelForParameterEstimation();
            }
            
        }
        public bool DataAppliesToModel
        {
            get
            {
                try
                {
                    return CheckDataAgainstModel();
                }
                catch
                {
                    // catch errors 
                    return false;
                }
                
            }
        }

        private static FittingItem GetBestItem(List<FittingItem> items, List<string> priorities)
        {
            foreach (var experiments in priorities)
            {
                foreach (var item in items)
                {
                    if (item.AffectedExperiments.Contains(experiments))
                        return item;
                }
            }
            return items.FirstOrDefault();
        }
        
        private double GetBestValueFor(string name, CheckPoint data, List<string> priorities)
        {
            var item = GetBestItem(GetItems(name), priorities);
            var index = FittingItems.IndexOf(item);
            return data.Parameters[index];
        }

        private void SetInitialConditionsFromParameterEstimation(CheckPoint data, List<string> priorities)
        {
            CFitTask fitTask = GetFitTask();

            if (fitTask == null)
                return;

            CFitProblem problem = (CFitProblem)fitTask.getProblem();
            problem.setModel(_DataModel.getModel());
            ObjectStdVector changedObjects = new ObjectStdVector();
            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);
                var modelItem = _DataModel.getObject(item.getObjectCN());
                CCopasiObject current = _DataModel.getDataObject(modelItem.getCN());
                double bestValue = GetBestValueFor(item.getObjectDisplayName(), data, priorities);
                if (current is CCopasiParameter)
                    ((CCopasiParameter)current).setDblValue(bestValue);
                else
                {
                    current.setObjectValue(bestValue);
                    changedObjects.Add(current);
                }
            }
            _DataModel.getModel().updateInitialValues(changedObjects);
        }

        private void SetInitialConditionsFromOptimization(CheckPoint data, List<string> priorities)
        {
            var optTask = GetOptTask();

            if (optTask == null)
                return;

            var problem = (COptProblem)optTask.getProblem();
            problem.setModel(_DataModel.getModel());
            ObjectStdVector changedObjects = new ObjectStdVector();
            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);
                var modelItem = _DataModel.getObject(item.getObjectCN());
                CCopasiObject current = _DataModel.getDataObject(modelItem.getCN());
                double bestValue = GetBestValueFor(item.getObjectDisplayName(), data, priorities);
                if (current is CCopasiParameter)
                    ((CCopasiParameter)current).setDblValue(bestValue);
                else
                {
                    current.setObjectValue(bestValue);
                    changedObjects.Add(current);
                }
            }
            _DataModel.getModel().updateInitialValues(changedObjects);
        }
        public void SetInitialConditions(CheckPoint data, List<string> priorities)
        {
            if (IsOptimization)
                SetInitialConditionsFromOptimization(data, priorities);
            else
                SetInitialConditionsFromParameterEstimation(data, priorities);
        }
        
        private CFitTask GetFitTask()
        {
            for (uint i = 0; i < _DataModel.getTaskList().size(); i++)
            {
                CCopasiTask task = _DataModel.getTask(i);
                if (task is CFitTask)                
                    return (CFitTask)task;
            }
            return null;
        }


        private COptTask GetOptTask()
        {
            var result = _DataModel.getTask("Optimization");
            if (result != null)
                return (COptTask)result;

            for (uint i = 0; i < _DataModel.getTaskList().size(); i++)
            {
                var task = _DataModel.getTask(i);
                if (task is COptTask)
                    return (COptTask)task;
            }
            return null;
        }

        public void SetParameterEstimationStartValues(CheckPoint data)
        {
            if (IsOptimization)
                return;

            CFitTask fitTask = GetFitTask();

            if (fitTask == null)
                return;
            
            CFitProblem problem = (CFitProblem)fitTask.getProblem();
            problem.setModel(_DataModel.getModel());

            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);
                item.setStartValue(data.Parameters[i]);
            }
        }

        public void SetStartValues(CheckPoint data)
        {
            if (IsOptimization)
                SetOptimizationStartValues(data);
            else SetParameterEstimationStartValues(data);
        }

        public void SetOptimizationStartValues(CheckPoint data)
        {
            if (!IsOptimization)
                return;

            COptTask optTask = GetOptTask();

            if (optTask == null)
                return;

            COptProblem problem = (COptProblem)optTask.getProblem();
            problem.setModel(_DataModel.getModel());

            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);
                item.setStartValue(data.Parameters[i]);
            }

        }

        
        private List<FittingItem> GetItems(string displayName)
        {
            displayName = displayName.Replace("\"", "");
            return (from item in FittingItems where item.Name == displayName select item).ToList();
        }

        private void UpdateParameterEstimationFromFile()
        {
            CFitTask fitTask = GetFitTask();

            if (fitTask == null)
                return;

            CFitProblem problem = (CFitProblem)fitTask.getProblem();
            problem.setModel(_DataModel.getModel());

            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);

                string displayName = item.getObjectDisplayName().Replace("\"", "");

                FittingItem currentItem = FittingItems[i];
                if (currentItem == null)
                    continue;

                if (currentItem.Name != displayName)
                {
                    System.Diagnostics.Debug.WriteLine("Names don't match !");
                    System.Diagnostics.Debugger.Break();
                }

                currentItem.StartValue = item.getStartValue();
                currentItem.LowerBound = item.getLowerBoundValue();
                currentItem.UpperBound = item.getUpperBoundValue();
            }
        }


        private void UpdateOptimizationFromFile()
        {
            var optTask = GetOptTask();

            if (optTask == null)
                return;

            COptProblem problem = (COptProblem)optTask.getProblem();
            problem.setModel(_DataModel.getModel());

            for (int i = 0; i < problem.getOptItemList().Count; i++)
            {
                COptItem item = (COptItem)(problem.getOptItemList()[i]);

                string displayName = item.getObjectDisplayName().Replace("\"", "");

                FittingItem currentItem = FittingItems[i];
                if (currentItem == null)
                    continue;

                if (currentItem.Name != displayName)
                {
                    System.Diagnostics.Debug.WriteLine("Names don't match !");
                    System.Diagnostics.Debugger.Break();
                }

                currentItem.StartValue = item.getStartValue();
                currentItem.LowerBound = item.getLowerBoundValue();
                currentItem.UpperBound = item.getUpperBoundValue();
            }
        }
        public void UpdateItemsFromFile()
        {
            if (IsOptimization)
                UpdateOptimizationFromFile();
            else 
                UpdateParameterEstimationFromFile();
        }
        public CopasiResult()
        {
            _DataModel = CCopasiRootContainer.addDatamodel();
            FittingItems = new List<FittingItem>();
            Data = new List<CheckPoint>();
            IsOptimization = false;
        }

        public bool IsOptimization { get; set; }

        public bool IsParameterEstimation
        {
            get
            {
                return !IsOptimization; ;
            }
            set
            {
                IsOptimization = !value;
            }
        }

        public override string ToString()
        {
            return String.Format("Items {0}, Data {1}", FittingItems.Count, Data.Count);
        }

    }
}
