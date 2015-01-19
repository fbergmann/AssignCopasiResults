using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LibCopasiResults;
using System.Collections;
using System.Diagnostics;

namespace AssignCopasiResults
{
    public partial class MainForm : Form
    {
        private const string STR_CurrentExecutable = "currentExecutable";

        public List<CopasiResult> Model { get; set; }

        public CopasiResult Current { get; set; }

        public string ViewExecutable { get; set; }

        public ApplicationState CurrentUiState { get; set; }

        public MainForm()
        {
            InitializeComponent();
            ViewExecutable = (string)Application.UserAppDataRegistry.GetValue(STR_CurrentExecutable, "");

        }


        private void UpdateUI()
        {
            try
            {
                gridFittingItems.SuspendLayout();
                gridData.SuspendLayout();
                
                try
                {

                    gridFittingItems.Rows.Clear();
                    gridData.Rows.Clear();
                    if (Current == null)
                        return;

                    gridData.Columns.Clear();

                    gridData.Columns.Add(new DataGridViewTextBoxColumn { Name = "Function Evaluations" });
                    gridData.Columns.Add(new DataGridViewTextBoxColumn { Name = "Best Fit" });



                    foreach (var item in Current.FittingItems)
                    {
                        gridData.Columns.Add(new DataGridViewTextBoxColumn { Name = item.Name });
                        gridFittingItems.Rows.Add(item.Name,
                            item.LowerBound,
                            item.UpperBound,
                            item.StartValue,
                            item.AffectedExperiments.AsString());
                    }


                    int index = 0;
                    foreach (var item in Current.Data)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = item.FunctionEvaluations });
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = item.BestValue });
                        row.Tag = index;

                        foreach (var value in item.Parameters)
                        {
                            row.Cells.Add(new DataGridViewTextBoxCell { Value = value });
                        }

                        gridData.Rows.Add(row);
                        index++;
                    }
                }
                finally
                {

                    
                    gridData.ResumeLayout();

                    gridFittingItems.ResumeLayout();
                }

                if (gridData.Rows.Count > 0)
                {
                    gridData.Rows[gridData.Rows.Count - 1].Selected = true;
                    gridData.CurrentCell = gridData.Rows[gridData.Rows.Count - 1].Cells[0];
                }

            }
            catch 
            {
                
            }
        }

        private void OpenFile(string fileName)
        {
            try
            {
                txtProtocoll.Text = fileName;
                
                lblDataSets.Visible = false;
                toolCmbData.Visible = false;
                toolCmbData.Items.Clear();
                toolSep1.Visible = false;

                Model = ResultParser.FromFile(fileName);

                if (Model.Count == 0)
                {
                    txtProtocoll.Text = "";
                    MessageBox.Show("The protocoll file you have loaded does not contain fitting items or data and cannot be used to update a COPASI model.",
                       "Invalid Protocoll loaded",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int i = 0; i < Model.Count; i++)
                {
                    toolCmbData.Items.Add("DataSet " + i);
                }

                if (Model.Count > 1)
                {
                    lblDataSets.Visible = true;
                    toolCmbData.Visible = true;
                    toolSep1.Visible = true;

                    
                }

                toolCmbData.SelectedIndex = Model.Count -1;

                //SelectDataSet(Model.Last());

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format(
                        "An Error occured when trying to parse the protocoll file. {0}{0}{1}",
                        Environment.NewLine, ex.Message),
                        "Error loading protocoll",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);    
            }
        }



        private void SelectDataSet(CopasiResult data)
        {
            Current = data;

            if (Current.FittingItems.Count == 0 || Current.Data.Count == 0)
            {
                txtProtocoll.Text = "";
                MessageBox.Show("The protocoll file you have loaded does not contain fitting items or data and cannot be used to update a COPASI model.",
                   "Invalid Protocoll loaded",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblNumParameters.Text = string.Format("Number of Parameters: {0}", Current.FittingItems.Count);
            lblNumRows.Text = string.Format("Number of Rows: {0}", Current.Data.Count);
            UpdateUI();
            CurrentUiState = ApplicationState.OpenedProtocoll;
            SwitchToState();
        }


        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Title = "Open Result File", Filter = "Text files|*.txt|All files|*.*" })
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OpenFile(dlg.FileName);
                }
            }
        }

        private void SwitchToState()
        {
            switch(CurrentUiState)
            {
                case ApplicationState.NewDocument:
                    {
                        txtProtocoll.Text = "";
                        txtCopasiFile.Text = "";
                        cmdApplyInitialConditions.Enabled = false;
                        cmdApplyStartValues.Enabled = false;
                        cmdBrowse.Enabled = false;
                        cmdBrowseProtocoll.Enabled = true;
                        txtCopasiFile.Enabled = false;
                        txtProtocoll.Enabled = true;
                        cmdLoadCopasi.Enabled = false;
                        cmdUpdate.Enabled = false;
                        saveToolStripButton.Enabled = false;
                        saveToolStripMenuItem.Enabled = false;
                        mnuOpenCopasi.Enabled = false;
                        mnuExportSBML.Enabled = false;
                             

                    }
                    break;
                case ApplicationState.OpenedProtocoll:
                    {
                        cmdApplyInitialConditions.Enabled = false;
                        cmdApplyStartValues.Enabled = false;
                        cmdBrowse.Enabled = true;
                        cmdBrowseProtocoll.Enabled = false;
                        txtCopasiFile.Enabled = true;
                        txtProtocoll.Enabled = false;
                        cmdLoadCopasi.Enabled = true;
                        cmdUpdate.Enabled = false;
                        saveToolStripButton.Enabled = false;
                        saveToolStripMenuItem.Enabled = false;
                        mnuOpenCopasi.Enabled = true;
                        mnuExportSBML.Enabled = false;
                    }
                    break;
                case ApplicationState.OpenedCopasiFile:
                    {
                        cmdApplyInitialConditions.Enabled = true;
                        cmdApplyStartValues.Enabled = true;
                        cmdBrowse.Enabled = true;
                        cmdBrowseProtocoll.Enabled = false;
                        txtCopasiFile.Enabled = false;
                        txtProtocoll.Enabled = false;
                        cmdLoadCopasi.Enabled = true;
                        cmdUpdate.Enabled = true;
                        saveToolStripButton.Enabled = false;
                        saveToolStripMenuItem.Enabled = false;
                        mnuOpenCopasi.Enabled = true;
                        mnuExportSBML.Enabled = true;
                    }
                    break;
                case ApplicationState.AppliedChanges:
                    {
                        cmdApplyInitialConditions.Enabled = true;
                        cmdApplyStartValues.Enabled = true;
                        cmdBrowse.Enabled = true;
                        cmdBrowseProtocoll.Enabled = false;
                        txtCopasiFile.Enabled = false;
                        txtProtocoll.Enabled = false;
                        cmdLoadCopasi.Enabled = true;
                        cmdUpdate.Enabled = true;
                        saveToolStripButton.Enabled = true;
                        saveToolStripMenuItem.Enabled = true;
                        mnuOpenCopasi.Enabled = true;
                        mnuExportSBML.Enabled = true;
                    }
                    break;
                
            }
        }
        private void NewDocument()
        {
            Current = new CopasiResult();
            CurrentUiState = ApplicationState.NewDocument;
            SwitchToState();
            UpdateUI();
        }
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void SaveFile(string fileName)
        {
            if (Current != null)
                Current.SaveCPS(fileName);
            CurrentUiState = ApplicationState.OpenedCopasiFile;
            SwitchToState();
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Title = "Save COPASI File", Filter = "COPASI files|*.cps;*.xml|All files|*.*" })
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SaveFile(dlg.FileName);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox())
            {
                aboutBox.ShowDialog();
            }
        }

        private void ApplyInitialConditions()
        {
            try
            {
                var list = new List<string>();
                foreach (var item in Current.FittingItems)
                    foreach (var exp in item.AffectedExperiments)
                        if (exp != "all" && !list.Contains(exp))
                            list.Add(exp);

                if (list.Count > 0)
                {
                    var dialog = new FormSortList { Items = list };

                    var result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.Cancel)
                        return;
                    if (result == System.Windows.Forms.DialogResult.OK)
                        list = dialog.Items;
                }

                int index = (int)gridData.CurrentRow.Tag;
                Current.SetInitialConditions(Current.Data[index], list);
                CurrentUiState = ApplicationState.AppliedChanges;
                SwitchToState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(
                    "An Error occured when trying to apply the selected dataset as initial conditions of the model {0}{0}. {1}", 
                    Environment.NewLine, ex.Message), 
                    "Error setting Initial Conditions", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        private void cmdApplyInitialConditions_Click(object sender, EventArgs e)
        {
            ApplyInitialConditions();      
        }

        private void OpenCopasi(string fileName)
        {
            try
            {
                txtCopasiFile.Text = fileName;
                Current.LoadCopasi(fileName);

                try
                {
                    Current.CheckDataAgainstModel();
                }
                catch (Exception ex)
                {
                    txtCopasiFile.Text = "";

                    MessageBox.Show(string.Format("The COPASI file you have loaded does not apply to the protocoll that you loaded. (The parameter estimation task does not contain the same list of parameters as the protocoll file). {0}{0}{1}",
                        Environment.NewLine,
                        ex.Message),
                        "Invalid Model loaded",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    
                }

                CurrentUiState = ApplicationState.OpenedCopasiFile;
                SwitchToState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format(
                        "An Error occured when trying to load the copasi file.{0}{0}{1}",
                        Environment.NewLine, ex.Message),
                        "Error Loading COPASI model",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);    
            }
        }
        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Title = "Open COPASI File", Filter = "COPASI files|*.cps;*.xml|All files|*.*" })
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OpenCopasi(dlg.FileName);
                }
            }
        }

        private void ApplyTaskStartValues()
        {
            try
            {
                int index = (int)gridData.CurrentRow.Tag;
                Current.SetStartValues(Current.Data[index]);
                Current.UpdateItemsFromFile();
                UpdateUI();
                gridData.Rows[index].Selected = true;
                CurrentUiState = ApplicationState.AppliedChanges;
                SwitchToState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format(
                        "An Error occured when trying to set the start values of the parameter estimation to the selected dataset. {0}{0}{1}",
                        Environment.NewLine, ex.Message),
                        "Error setting Parameter estimation start values",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);    
            }
        }
        private void cmdApplyStartValues_Click(object sender, EventArgs e)
        {
            ApplyTaskStartValues();
        }

        private void cmdLoadCopasi_Click(object sender, EventArgs e)
        {
            string copasiFile = txtCopasiFile.Text;
            if (string.IsNullOrWhiteSpace(copasiFile) || !File.Exists(copasiFile))
                cmdBrowse_Click(sender, e);
            else
                OpenCopasi(copasiFile);
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Current.UpdateItemsFromFile();
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format(
                        "An Error occured when trying update the fititems from the model. {0}{0}{1}",
                        Environment.NewLine, ex.Message),
                        "Error updating FitItems",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);    
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void mnuExportSBML_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Title = "Export SBML File", Filter = "SBML files|*.sbml;*.xml|All files|*.*" })
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Current.ExportSBML(dlg.FileName);
                }
            }
        }



        private void toolCmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = toolCmbData.SelectedIndex;
            if (index != -1 && index < Model.Count)
                SelectDataSet(Model[index]);
        }

        private void gridData_DoubleClick(object sender, EventArgs e)
        {
            string copasiFile
                = txtCopasiFile.Text;

            if (string.IsNullOrWhiteSpace(copasiFile) || !File.Exists(copasiFile))
                return;

            ApplyTaskStartValues();

            string dir = Path.GetDirectoryName(copasiFile);
            var file = CopasiResult.GetTempFileInPath(dir);
            Current.SaveCPS(file);

            //var file = Current.SaveTempIn(dir);
            bool hide = false;
            Process.Start(ViewExecutable,
                String.Format("\"{0}\" --set-solution-statistic --disable-calculate-statistic --disable-randomize-startvalues --clear-targets {1}{2}",
                                              file,
                                              (Current.IsParameterEstimation ? " --disable-other-plots -g 910 -r -s 33" : " -g 914 -r -s 32"),
                                              hide ? " --hide" : ""));
        }

        private void OnEditPreferences(object sender, EventArgs e)
        {
            var dialog = new FormPreferences { CurrentExecutable = ViewExecutable };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewExecutable = dialog.CurrentExecutable;
                Application.UserAppDataRegistry.SetValue(STR_CurrentExecutable, ViewExecutable);
            }
        }
    }
}
