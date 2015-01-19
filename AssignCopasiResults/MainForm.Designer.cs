namespace AssignCopasiResults
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblNumParameters = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumRows = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridFittingItems = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLowerBound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpperBound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAffectedExperiments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdBrowseProtocoll = new System.Windows.Forms.Button();
            this.txtProtocoll = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdLoadCopasi = new System.Windows.Forms.Button();
            this.cmdApplyInitialConditions = new System.Windows.Forms.Button();
            this.cmdApplyStartValues = new System.Windows.Forms.Button();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.txtCopasiFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenProtocoll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenCopasi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportSBML = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblDataSets = new System.Windows.Forms.ToolStripLabel();
            this.toolCmbData = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPreferences = new System.Windows.Forms.ToolStripButton();
            this.toolSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.gridFittingItems)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(765, 366);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(765, 437);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblNumParameters,
            this.lblNumRows});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(765, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // lblNumParameters
            // 
            this.lblNumParameters.Name = "lblNumParameters";
            this.lblNumParameters.Size = new System.Drawing.Size(10, 17);
            this.lblNumParameters.Text = " ";
            // 
            // lblNumRows
            // 
            this.lblNumRows.Name = "lblNumRows";
            this.lblNumRows.Size = new System.Drawing.Size(10, 17);
            this.lblNumRows.Text = " ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(765, 366);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 97);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridFittingItems);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridData);
            this.splitContainer1.Size = new System.Drawing.Size(759, 266);
            this.splitContainer1.SplitterDistance = 61;
            this.splitContainer1.TabIndex = 0;
            // 
            // gridFittingItems
            // 
            this.gridFittingItems.AllowUserToAddRows = false;
            this.gridFittingItems.AllowUserToDeleteRows = false;
            this.gridFittingItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridFittingItems.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridFittingItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFittingItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colLowerBound,
            this.colUpperBound,
            this.colStartValue,
            this.colAffectedExperiments});
            this.gridFittingItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFittingItems.Location = new System.Drawing.Point(0, 0);
            this.gridFittingItems.Name = "gridFittingItems";
            this.gridFittingItems.ReadOnly = true;
            this.gridFittingItems.Size = new System.Drawing.Size(759, 61);
            this.gridFittingItems.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colLowerBound
            // 
            this.colLowerBound.HeaderText = "Lower Bound";
            this.colLowerBound.Name = "colLowerBound";
            this.colLowerBound.ReadOnly = true;
            // 
            // colUpperBound
            // 
            this.colUpperBound.HeaderText = "Upper Bound";
            this.colUpperBound.Name = "colUpperBound";
            this.colUpperBound.ReadOnly = true;
            // 
            // colStartValue
            // 
            this.colStartValue.HeaderText = "Start Value";
            this.colStartValue.Name = "colStartValue";
            this.colStartValue.ReadOnly = true;
            // 
            // colAffectedExperiments
            // 
            this.colAffectedExperiments.HeaderText = "Affected Experiments";
            this.colAffectedExperiments.Name = "colAffectedExperiments";
            this.colAffectedExperiments.ReadOnly = true;
            // 
            // gridData
            // 
            this.gridData.AllowUserToAddRows = false;
            this.gridData.AllowUserToDeleteRows = false;
            this.gridData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData.Location = new System.Drawing.Point(0, 0);
            this.gridData.MultiSelect = false;
            this.gridData.Name = "gridData";
            this.gridData.ReadOnly = true;
            this.gridData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridData.Size = new System.Drawing.Size(759, 201);
            this.gridData.TabIndex = 0;
            this.gridData.DoubleClick += new System.EventHandler(this.gridData_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdBrowseProtocoll);
            this.panel1.Controls.Add(this.txtProtocoll);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmdUpdate);
            this.panel1.Controls.Add(this.cmdLoadCopasi);
            this.panel1.Controls.Add(this.cmdApplyInitialConditions);
            this.panel1.Controls.Add(this.cmdApplyStartValues);
            this.panel1.Controls.Add(this.cmdBrowse);
            this.panel1.Controls.Add(this.txtCopasiFile);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 88);
            this.panel1.TabIndex = 1;
            // 
            // cmdBrowseProtocoll
            // 
            this.cmdBrowseProtocoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseProtocoll.Location = new System.Drawing.Point(675, 9);
            this.cmdBrowseProtocoll.Name = "cmdBrowseProtocoll";
            this.cmdBrowseProtocoll.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseProtocoll.TabIndex = 9;
            this.cmdBrowseProtocoll.Text = "...";
            this.cmdBrowseProtocoll.UseVisualStyleBackColor = true;
            this.cmdBrowseProtocoll.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // txtProtocoll
            // 
            this.txtProtocoll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProtocoll.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProtocoll.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtProtocoll.Location = new System.Drawing.Point(90, 9);
            this.txtProtocoll.Name = "txtProtocoll";
            this.txtProtocoll.Size = new System.Drawing.Size(579, 20);
            this.txtProtocoll.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Protocoll File: ";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(222, 61);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(131, 23);
            this.cmdUpdate.TabIndex = 6;
            this.cmdUpdate.Text = "Update FitItems";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdLoadCopasi
            // 
            this.cmdLoadCopasi.Location = new System.Drawing.Point(90, 61);
            this.cmdLoadCopasi.Name = "cmdLoadCopasi";
            this.cmdLoadCopasi.Size = new System.Drawing.Size(126, 23);
            this.cmdLoadCopasi.TabIndex = 5;
            this.cmdLoadCopasi.Text = "Load Copasi File";
            this.cmdLoadCopasi.UseVisualStyleBackColor = true;
            this.cmdLoadCopasi.Click += new System.EventHandler(this.cmdLoadCopasi_Click);
            // 
            // cmdApplyInitialConditions
            // 
            this.cmdApplyInitialConditions.Location = new System.Drawing.Point(582, 61);
            this.cmdApplyInitialConditions.Name = "cmdApplyInitialConditions";
            this.cmdApplyInitialConditions.Size = new System.Drawing.Size(168, 23);
            this.cmdApplyInitialConditions.TabIndex = 4;
            this.cmdApplyInitialConditions.Text = "Apply to Model State";
            this.cmdApplyInitialConditions.UseVisualStyleBackColor = true;
            this.cmdApplyInitialConditions.Click += new System.EventHandler(this.cmdApplyInitialConditions_Click);
            // 
            // cmdApplyStartValues
            // 
            this.cmdApplyStartValues.Location = new System.Drawing.Point(359, 61);
            this.cmdApplyStartValues.Name = "cmdApplyStartValues";
            this.cmdApplyStartValues.Size = new System.Drawing.Size(217, 23);
            this.cmdApplyStartValues.TabIndex = 3;
            this.cmdApplyStartValues.Text = "Set Task Start Values";
            this.cmdApplyStartValues.UseVisualStyleBackColor = true;
            this.cmdApplyStartValues.Click += new System.EventHandler(this.cmdApplyStartValues_Click);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Location = new System.Drawing.Point(675, 35);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.Text = "...";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // txtCopasiFile
            // 
            this.txtCopasiFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCopasiFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCopasiFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtCopasiFile.Location = new System.Drawing.Point(90, 35);
            this.txtCopasiFile.Name = "txtCopasiFile";
            this.txtCopasiFile.Size = new System.Drawing.Size(579, 20);
            this.txtCopasiFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Copasi File: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(765, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.mnuOpenProtocoll,
            this.mnuOpenCopasi,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.mnuExportSBML,
            this.toolStripSeparator4,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // mnuOpenProtocoll
            // 
            this.mnuOpenProtocoll.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpenProtocoll.Image")));
            this.mnuOpenProtocoll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpenProtocoll.Name = "mnuOpenProtocoll";
            this.mnuOpenProtocoll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenProtocoll.Size = new System.Drawing.Size(238, 22);
            this.mnuOpenProtocoll.Text = "&Open Protocoll";
            this.mnuOpenProtocoll.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // mnuOpenCopasi
            // 
            this.mnuOpenCopasi.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpenCopasi.Image")));
            this.mnuOpenCopasi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpenCopasi.Name = "mnuOpenCopasi";
            this.mnuOpenCopasi.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.mnuOpenCopasi.Size = new System.Drawing.Size(238, 22);
            this.mnuOpenCopasi.Text = "&Open Copasi File";
            this.mnuOpenCopasi.Click += new System.EventHandler(this.cmdLoadCopasi_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(235, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // mnuExportSBML
            // 
            this.mnuExportSBML.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportSBML.Image")));
            this.mnuExportSBML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExportSBML.Name = "mnuExportSBML";
            this.mnuExportSBML.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuExportSBML.Size = new System.Drawing.Size(238, 22);
            this.mnuExportSBML.Text = "&Export SBML";
            this.mnuExportSBML.Click += new System.EventHandler(this.mnuExportSBML_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(235, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(238, 22);
            this.toolStripMenuItem1.Text = "&Prefernces";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.OnEditPreferences);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(235, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator2,
            this.lblDataSets,
            this.toolCmbData,
            this.toolStripSeparator3,
            this.toolPreferences,
            this.toolSep1,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(406, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblDataSets
            // 
            this.lblDataSets.Name = "lblDataSets";
            this.lblDataSets.Size = new System.Drawing.Size(58, 22);
            this.lblDataSets.Text = "DataSets: ";
            this.lblDataSets.Visible = false;
            // 
            // toolCmbData
            // 
            this.toolCmbData.Name = "toolCmbData";
            this.toolCmbData.Size = new System.Drawing.Size(121, 25);
            this.toolCmbData.Visible = false;
            this.toolCmbData.SelectedIndexChanged += new System.EventHandler(this.toolCmbData_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolPreferences
            // 
            this.toolPreferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolPreferences.Image = ((System.Drawing.Image)(resources.GetObject("toolPreferences.Image")));
            this.toolPreferences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPreferences.Name = "toolPreferences";
            this.toolPreferences.Size = new System.Drawing.Size(72, 22);
            this.toolPreferences.Text = "&Preferences";
            this.toolPreferences.Click += new System.EventHandler(this.OnEditPreferences);
            // 
            // toolSep1
            // 
            this.toolSep1.Name = "toolSep1";
            this.toolSep1.Size = new System.Drawing.Size(6, 25);
            this.toolSep1.Visible = false;
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 437);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(512, 475);
            this.Name = "MainForm";
            this.Text = "Assign Copasi Results";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.gridFittingItems)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridFittingItems;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenCopasi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolSep1;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblNumParameters;
        private System.Windows.Forms.ToolStripStatusLabel lblNumRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLowerBound;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpperBound;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAffectedExperiments;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdApplyInitialConditions;
        private System.Windows.Forms.Button cmdApplyStartValues;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.TextBox txtCopasiFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdLoadCopasi;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdBrowseProtocoll;
        private System.Windows.Forms.TextBox txtProtocoll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenProtocoll;
        private System.Windows.Forms.ToolStripMenuItem mnuExportSBML;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblDataSets;
        private System.Windows.Forms.ToolStripComboBox toolCmbData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolPreferences;
    }
}

