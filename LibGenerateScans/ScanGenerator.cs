using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using org.COPASI;

namespace LibGenerateScans
{
    public class ScanGenerator
    {
        private const int INT_LEVENBERG = 12;
        private const int INT_HOOKE = 11;
        private static CFitProblem _Problem;
        private static CFitTask _Task;
        public GenerateArguments Arguments { get; set; }
        public CCopasiDataModel _DataModel { get; set; }

        public TextWriter Out { get; set; }

        private CTrajectoryTask GetTimeCourseTask()
        {
            for (uint i = 0; i < _DataModel.getTaskList().size(); i++)
            {
                CCopasiTask task = _DataModel.getTask(i);
                if (task is CTrajectoryTask)
                    return (CTrajectoryTask)task;
            }
            return null;
        }
        private CScanTask GetScanTask()
        {
            for (uint i = 0; i < _DataModel.getTaskList().size(); i++)
            {
                CCopasiTask task = _DataModel.getTask(i);
                if (task is CScanTask)
                    return (CScanTask)task;
            }
            return null;
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

        private List<OptItem> ConvertItems(OptItemStdVector optItems)
        {
            List<OptItem> result = new List<OptItem>();
            for (int i = 0; i < optItems.Count; i++)
                result.Add(new OptItem { CN = optItems[i].getObjectCN().getString(), StartValue = optItems[i].getStartValue(), Index = (uint)i });
            return result;
        }

        private void GenerateScanForItem(OptItem item, int index, bool updateModel = false, bool lower = false)
        {
            _DataModel.loadModel(Arguments.FileName);

            if (Arguments.DisableOtherPlots)
            {
                Out.WriteLine("... Disable other Plots");
                uint numPlots = _DataModel.getPlotDefinitionList().size();
                for (uint i = 0; i < numPlots; i++)
                {
                    CPlotSpecification current = _DataModel.getPlotSpecification(i);
                    current.setActive(false);

                }
            }

            if (Arguments.DisableOtherTasks)
            {
                Out.WriteLine("... Disable other Tasks");
                uint numTasks = _DataModel.getTaskList().size();
                for (uint i = 0; i < numTasks; i++)
                {
                    CCopasiTask current = _DataModel.getTask(i);
                    current.setScheduled(false);
                }
            }

            var timeCourse = GetTimeCourseTask();
            timeCourse.getMethod().getParameter("Relative Tolerance").setDblValue(1e-9);

            var task = GetFitTask();
            task.setUpdateModel(updateModel);

            if (Arguments.UseHooke)
            {
                task.setMethodType(INT_HOOKE);
            }
            else
            {
                task.setMethodType(INT_LEVENBERG);
                task.getMethod().getParameter("Iteration Limit").setIntValue(Arguments.Iterations);
                if (task.getMethod().getParameter("Modulation") != null)
                task.getMethod().getParameter("Modulation").setDblValue(Arguments.Modulation);
                task.getMethod().getParameter("Tolerance").setDblValue(Arguments.LMTolerance);
            }

            var problem = (CFitProblem)task.getProblem();
            Out.WriteLine("... Remove OptItem");
            problem.removeOptItem(item.Index);


            Out.WriteLine("... Generate Scan");
            CScanTask scanTask = GetScanTask();
            scanTask.setScheduled(true);
            CScanProblem scanProblem = (CScanProblem)scanTask.getProblem();
            scanProblem.setSubtask(5);
            scanProblem.setContinueFromCurrentState(false);
            scanProblem.setOutputInSubtask(false);
            scanProblem.clearScanItems();
            scanProblem.addScanItem(1, Arguments.ScanInterval);
            var scanItem = scanProblem.getScanItem(0);
            scanItem.getParameter("Object").setCNValue(new CRegisteredObjectName(item.CN));

            string suffix;

            if (!updateModel)
            {
                suffix = "_noupdate";
                scanItem.getParameter("Maximum").setDblValue(item.StartValue * Arguments.UpperMultiplier);
                scanItem.getParameter("Minimum").setDblValue(item.StartValue * Arguments.LowerMultiplier);
            }
            else if (lower)
            {
                suffix = "_update_low";
                scanItem.getParameter("Minimum").setDblValue(item.StartValue);
                scanItem.getParameter("Maximum").setDblValue(item.StartValue * Arguments.LowerMultiplier);
            }
            else
            {
                suffix = "_update_high";
                scanItem.getParameter("Maximum").setDblValue(item.StartValue * Arguments.UpperMultiplier);
                scanItem.getParameter("Minimum").setDblValue(item.StartValue);
            }

            scanTask.updateMatrices();

            Out.WriteLine("... Generate Report");
            COutputAssistant.createDefaultOutput(1251, scanTask, _DataModel);
            scanTask.getReport().setTarget(string.Format("{0}{1:D3}{2}.txt", Arguments.Prefix, index, suffix));

            Out.WriteLine("... Generate Plot");
            COutputAssistant.createDefaultOutput(251, scanTask, _DataModel);

            string file = string.Format("{0}{1:D3}{2}.cps", Arguments.Prefix, index, suffix);
            Out.WriteLine("Writing '{0}'.", file);
            _DataModel.saveModel(
                Path.Combine(Path.GetDirectoryName(Arguments.FileName),
                file), true);
        }

        /// <summary>
        /// Initializes a new instance of the ScanGenerator class.
        /// </summary>
        public ScanGenerator(GenerateArguments arguments) : this()
        {
            Arguments = arguments;
        }


        /// <summary>
        /// Initializes a new instance of the ScanGenerator class.
        /// </summary>
        public ScanGenerator()
        {
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = culture;
            Arguments = new GenerateArguments();
            Out = Console.Out;
        }


        public bool IsValid
        {
            get { return Arguments != null && Arguments.IsValid; }
        }

        public void Process()
        {
            Out.WriteLine("GenerateScansForParameterEstimation");
            Out.WriteLine("===================================");

            if (!Arguments.IsValid)
                GenerateArguments.PrintUsageAndExit();

            Arguments.PrintStatus();

            _DataModel = CCopasiRootContainer.addDatamodel();

            if (!_DataModel.loadModel(Arguments.FileName))
            {
                Console.Error.WriteLine("Could not load model");
                return;
            }

            _Task = GetFitTask();
            if (_Task == null)
            {
                Console.Error.WriteLine("No Fit Task.");
                return;
            }

            _Problem = (CFitProblem)_Task.getProblem();
            var optItems = ConvertItems(_Problem.getOptItemList());
            Out.WriteLine("Have: {0} optItems", optItems.Count);
            Out.WriteLine();



            for (int i = 0; i < optItems.Count; i++)
            {
                Out.WriteLine("Handling: {0}", optItems[i].CN);
                GenerateScanForItem(optItems[i], i);
                GenerateScanForItem(optItems[i], i, true, true);
                GenerateScanForItem(optItems[i], i, true, false);
            }

        }
    }
}
