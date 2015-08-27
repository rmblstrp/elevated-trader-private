namespace ElevatedTrader.Windows.Forms
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
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NewSolutionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.LoadDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StopLoadingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SimulationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RunSimulationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StopSimulationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.symbolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AddSymbolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.SimulationProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.StateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.TickCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.traderToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.DataSourceComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.TickProviderComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.MaxTicksTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.SimulationIterations = new System.Windows.Forms.ToolStripTextBox();
			this.RunSimulationButton = new System.Windows.Forms.ToolStripButton();
			this.StopSimulationButton = new System.Windows.Forms.ToolStripButton();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.ResultsPanel = new System.Windows.Forms.Panel();
			this.SingleSimulationControl = new ElevatedTrader.Windows.Forms.SingleSimulation();
			this.PropertiesPanel = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.StrategiesComboBox = new System.Windows.Forms.ComboBox();
			this.StrategySettings = new System.Windows.Forms.PropertyGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SymbolComboBox = new System.Windows.Forms.ComboBox();
			this.SymbolProperties = new System.Windows.Forms.PropertyGrid();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.SimulationAnalysis = new System.Windows.Forms.PropertyGrid();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.SaveResultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenu.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.traderToolStrip.SuspendLayout();
			this.MainPanel.SuspendLayout();
			this.ResultsPanel.SuspendLayout();
			this.PropertiesPanel.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.BackColor = System.Drawing.SystemColors.MenuBar;
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.toolStripMenuItem1,
            this.SimulationMenuItem,
            this.symbolsToolStripMenuItem});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(1183, 24);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "menuStrip1";
			// 
			// FileMenuItem
			// 
			this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSolutionMenuItem,
            this.OpenMenuItem,
            this.SaveMenuItem,
            this.SaveAsMenuItem,
            this.toolStripSeparator1,
            this.settingsToolStripMenuItem});
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
			this.FileMenuItem.Text = "File";
			// 
			// NewSolutionMenuItem
			// 
			this.NewSolutionMenuItem.Name = "NewSolutionMenuItem";
			this.NewSolutionMenuItem.Size = new System.Drawing.Size(195, 22);
			this.NewSolutionMenuItem.Text = "New Solution";
			// 
			// OpenMenuItem
			// 
			this.OpenMenuItem.Name = "OpenMenuItem";
			this.OpenMenuItem.Size = new System.Drawing.Size(195, 22);
			this.OpenMenuItem.Text = "Open...";
			this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
			// 
			// SaveMenuItem
			// 
			this.SaveMenuItem.Name = "SaveMenuItem";
			this.SaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.SaveMenuItem.Size = new System.Drawing.Size(195, 22);
			this.SaveMenuItem.Text = "Save";
			this.SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
			// 
			// SaveAsMenuItem
			// 
			this.SaveAsMenuItem.Name = "SaveAsMenuItem";
			this.SaveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.SaveAsMenuItem.Size = new System.Drawing.Size(195, 22);
			this.SaveAsMenuItem.Text = "Save As...";
			this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.settingsToolStripMenuItem.Text = "Settings";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadDataMenuItem,
            this.StopLoadingMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
			this.toolStripMenuItem1.Text = "Data";
			// 
			// LoadDataMenuItem
			// 
			this.LoadDataMenuItem.Name = "LoadDataMenuItem";
			this.LoadDataMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.LoadDataMenuItem.Size = new System.Drawing.Size(119, 22);
			this.LoadDataMenuItem.Text = "Load";
			this.LoadDataMenuItem.Click += new System.EventHandler(this.LoadDataMenuItem_Click);
			// 
			// StopLoadingMenuItem
			// 
			this.StopLoadingMenuItem.Name = "StopLoadingMenuItem";
			this.StopLoadingMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.StopLoadingMenuItem.Size = new System.Drawing.Size(119, 22);
			this.StopLoadingMenuItem.Text = "Stop";
			this.StopLoadingMenuItem.Click += new System.EventHandler(this.StopLoadingMenuItem_Click);
			// 
			// SimulationMenuItem
			// 
			this.SimulationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunSimulationMenuItem,
            this.StopSimulationMenuItem,
            this.toolStripSeparator2,
            this.SaveResultsMenuItem});
			this.SimulationMenuItem.Name = "SimulationMenuItem";
			this.SimulationMenuItem.Size = new System.Drawing.Size(76, 20);
			this.SimulationMenuItem.Text = "Simulation";
			// 
			// RunSimulationMenuItem
			// 
			this.RunSimulationMenuItem.Name = "RunSimulationMenuItem";
			this.RunSimulationMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.RunSimulationMenuItem.Size = new System.Drawing.Size(152, 22);
			this.RunSimulationMenuItem.Text = "Run";
			this.RunSimulationMenuItem.Click += new System.EventHandler(this.RunSimulation_Click);
			// 
			// StopSimulationMenuItem
			// 
			this.StopSimulationMenuItem.Name = "StopSimulationMenuItem";
			this.StopSimulationMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.StopSimulationMenuItem.Size = new System.Drawing.Size(152, 22);
			this.StopSimulationMenuItem.Text = "Stop";
			this.StopSimulationMenuItem.Click += new System.EventHandler(this.StopSimulation_Click);
			// 
			// symbolsToolStripMenuItem
			// 
			this.symbolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSymbolMenuItem});
			this.symbolsToolStripMenuItem.Name = "symbolsToolStripMenuItem";
			this.symbolsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.symbolsToolStripMenuItem.Text = "Symbols";
			// 
			// AddSymbolMenuItem
			// 
			this.AddSymbolMenuItem.Name = "AddSymbolMenuItem";
			this.AddSymbolMenuItem.Size = new System.Drawing.Size(139, 22);
			this.AddSymbolMenuItem.Text = "Add Symbol";
			this.AddSymbolMenuItem.Click += new System.EventHandler(this.AddSymbolMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SimulationProgress,
            this.toolStripStatusLabel2,
            this.StateStatusLabel,
            this.toolStripStatusLabel1,
            this.TickCountStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 677);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1183, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// SimulationProgress
			// 
			this.SimulationProgress.Name = "SimulationProgress";
			this.SimulationProgress.Size = new System.Drawing.Size(100, 16);
			this.SimulationProgress.Step = 100;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(36, 17);
			this.toolStripStatusLabel2.Text = "State:";
			// 
			// StateStatusLabel
			// 
			this.StateStatusLabel.Name = "StateStatusLabel";
			this.StateStatusLabel.Size = new System.Drawing.Size(26, 17);
			this.StateStatusLabel.Text = "Idle";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(43, 17);
			this.toolStripStatusLabel1.Text = "Count:";
			// 
			// TickCountStatusLabel
			// 
			this.TickCountStatusLabel.Name = "TickCountStatusLabel";
			this.TickCountStatusLabel.Size = new System.Drawing.Size(13, 17);
			this.TickCountStatusLabel.Text = "0";
			// 
			// traderToolStrip
			// 
			this.traderToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.DataSourceComboBox,
            this.toolStripLabel2,
            this.TickProviderComboBox,
            this.toolStripLabel4,
            this.MaxTicksTextBox,
            this.toolStripLabel3,
            this.SimulationIterations,
            this.RunSimulationButton,
            this.StopSimulationButton});
			this.traderToolStrip.Location = new System.Drawing.Point(0, 24);
			this.traderToolStrip.Name = "traderToolStrip";
			this.traderToolStrip.Size = new System.Drawing.Size(1183, 25);
			this.traderToolStrip.TabIndex = 6;
			this.traderToolStrip.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(76, 22);
			this.toolStripLabel1.Text = "Data Source: ";
			// 
			// DataSourceComboBox
			// 
			this.DataSourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DataSourceComboBox.Name = "DataSourceComboBox";
			this.DataSourceComboBox.Size = new System.Drawing.Size(121, 25);
			this.DataSourceComboBox.Sorted = true;
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(82, 22);
			this.toolStripLabel2.Text = "Tick Provider: ";
			// 
			// TickProviderComboBox
			// 
			this.TickProviderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TickProviderComboBox.Name = "TickProviderComboBox";
			this.TickProviderComboBox.Size = new System.Drawing.Size(121, 25);
			this.TickProviderComboBox.Sorted = true;
			// 
			// toolStripLabel4
			// 
			this.toolStripLabel4.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.toolStripLabel4.Name = "toolStripLabel4";
			this.toolStripLabel4.Size = new System.Drawing.Size(65, 22);
			this.toolStripLabel4.Text = "Max Ticks: ";
			// 
			// MaxTicksTextBox
			// 
			this.MaxTicksTextBox.Name = "MaxTicksTextBox";
			this.MaxTicksTextBox.Size = new System.Drawing.Size(100, 25);
			this.MaxTicksTextBox.TextChanged += new System.EventHandler(this.MaxTickTextBox_TextChanged);
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(62, 22);
			this.toolStripLabel3.Text = "Iterations: ";
			// 
			// SimulationIterations
			// 
			this.SimulationIterations.Name = "SimulationIterations";
			this.SimulationIterations.Size = new System.Drawing.Size(100, 25);
			this.SimulationIterations.TextChanged += new System.EventHandler(this.SimulationIterations_TextChanged);
			// 
			// RunSimulationButton
			// 
			this.RunSimulationButton.Image = global::ElevatedTrader.Windows.Forms.ToolstripResources.control_play;
			this.RunSimulationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RunSimulationButton.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.RunSimulationButton.Name = "RunSimulationButton";
			this.RunSimulationButton.Size = new System.Drawing.Size(108, 22);
			this.RunSimulationButton.Text = "Run Simulation";
			this.RunSimulationButton.Click += new System.EventHandler(this.RunSimulation_Click);
			// 
			// StopSimulationButton
			// 
			this.StopSimulationButton.Image = global::ElevatedTrader.Windows.Forms.ToolstripResources.control_stop;
			this.StopSimulationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.StopSimulationButton.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.StopSimulationButton.Name = "StopSimulationButton";
			this.StopSimulationButton.Size = new System.Drawing.Size(111, 22);
			this.StopSimulationButton.Text = "Stop Simulation";
			this.StopSimulationButton.Visible = false;
			this.StopSimulationButton.Click += new System.EventHandler(this.StopSimulation_Click);
			// 
			// MainPanel
			// 
			this.MainPanel.Controls.Add(this.ResultsPanel);
			this.MainPanel.Controls.Add(this.PropertiesPanel);
			this.MainPanel.Controls.Add(this.groupBox3);
			this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainPanel.Location = new System.Drawing.Point(0, 49);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(1183, 628);
			this.MainPanel.TabIndex = 7;
			// 
			// ResultsPanel
			// 
			this.ResultsPanel.Controls.Add(this.SingleSimulationControl);
			this.ResultsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResultsPanel.Location = new System.Drawing.Point(300, 0);
			this.ResultsPanel.Name = "ResultsPanel";
			this.ResultsPanel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
			this.ResultsPanel.Size = new System.Drawing.Size(633, 628);
			this.ResultsPanel.TabIndex = 4;
			// 
			// SingleSimulationControl
			// 
			this.SingleSimulationControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SingleSimulationControl.Location = new System.Drawing.Point(5, 0);
			this.SingleSimulationControl.Name = "SingleSimulationControl";
			this.SingleSimulationControl.Size = new System.Drawing.Size(623, 623);
			this.SingleSimulationControl.TabIndex = 0;
			// 
			// PropertiesPanel
			// 
			this.PropertiesPanel.Controls.Add(this.groupBox2);
			this.PropertiesPanel.Controls.Add(this.groupBox1);
			this.PropertiesPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.PropertiesPanel.Location = new System.Drawing.Point(933, 0);
			this.PropertiesPanel.Name = "PropertiesPanel";
			this.PropertiesPanel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 5);
			this.PropertiesPanel.Size = new System.Drawing.Size(250, 628);
			this.PropertiesPanel.TabIndex = 5;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.MenuBar;
			this.groupBox2.Controls.Add(this.StrategiesComboBox);
			this.groupBox2.Controls.Add(this.StrategySettings);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 275);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(245, 348);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Strategy";
			// 
			// StrategiesComboBox
			// 
			this.StrategiesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.StrategiesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.StrategiesComboBox.FormattingEnabled = true;
			this.StrategiesComboBox.Location = new System.Drawing.Point(6, 19);
			this.StrategiesComboBox.Name = "StrategiesComboBox";
			this.StrategiesComboBox.Size = new System.Drawing.Size(231, 21);
			this.StrategiesComboBox.Sorted = true;
			this.StrategiesComboBox.TabIndex = 1;
			// 
			// StrategySettings
			// 
			this.StrategySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.StrategySettings.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.StrategySettings.HelpVisible = false;
			this.StrategySettings.Location = new System.Drawing.Point(6, 46);
			this.StrategySettings.Name = "StrategySettings";
			this.StrategySettings.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.StrategySettings.Size = new System.Drawing.Size(231, 296);
			this.StrategySettings.TabIndex = 0;
			this.StrategySettings.ToolbarVisible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.MenuBar;
			this.groupBox1.Controls.Add(this.SymbolComboBox);
			this.groupBox1.Controls.Add(this.SymbolProperties);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(245, 275);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Symbol";
			// 
			// SymbolComboBox
			// 
			this.SymbolComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SymbolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SymbolComboBox.FormattingEnabled = true;
			this.SymbolComboBox.Location = new System.Drawing.Point(6, 20);
			this.SymbolComboBox.Name = "SymbolComboBox";
			this.SymbolComboBox.Size = new System.Drawing.Size(231, 21);
			this.SymbolComboBox.Sorted = true;
			this.SymbolComboBox.TabIndex = 1;
			this.SymbolComboBox.SelectedIndexChanged += new System.EventHandler(this.SymbolComboBox_SelectedIndexChanged);
			// 
			// SymbolProperties
			// 
			this.SymbolProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SymbolProperties.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.SymbolProperties.HelpVisible = false;
			this.SymbolProperties.Location = new System.Drawing.Point(6, 47);
			this.SymbolProperties.Name = "SymbolProperties";
			this.SymbolProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.SymbolProperties.Size = new System.Drawing.Size(231, 222);
			this.SymbolProperties.TabIndex = 0;
			this.SymbolProperties.ToolbarVisible = false;
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.SystemColors.MenuBar;
			this.groupBox3.Controls.Add(this.SimulationAnalysis);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(300, 628);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Simulation Analysis";
			// 
			// SimulationAnalysis
			// 
			this.SimulationAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SimulationAnalysis.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.SimulationAnalysis.HelpVisible = false;
			this.SimulationAnalysis.Location = new System.Drawing.Point(6, 19);
			this.SimulationAnalysis.Name = "SimulationAnalysis";
			this.SimulationAnalysis.PropertySort = System.Windows.Forms.PropertySort.NoSort;
			this.SimulationAnalysis.Size = new System.Drawing.Size(286, 603);
			this.SimulationAnalysis.TabIndex = 0;
			this.SimulationAnalysis.ToolbarVisible = false;
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// SaveResultsMenuItem
			// 
			this.SaveResultsMenuItem.Name = "SaveResultsMenuItem";
			this.SaveResultsMenuItem.Size = new System.Drawing.Size(152, 22);
			this.SaveResultsMenuItem.Text = "Save Results";
			this.SaveResultsMenuItem.Click += new System.EventHandler(this.SaveResultsMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1183, 699);
			this.Controls.Add(this.MainPanel);
			this.Controls.Add(this.traderToolStrip);
			this.Controls.Add(this.MainMenu);
			this.Controls.Add(this.statusStrip1);
			this.MainMenuStrip = this.MainMenu;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Elevated Trader";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.traderToolStrip.ResumeLayout(false);
			this.traderToolStrip.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.ResultsPanel.ResumeLayout(false);
			this.PropertiesPanel.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SimulationMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RunSimulationMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem NewSolutionMenuItem;
		private System.Windows.Forms.ToolStripMenuItem symbolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AddSymbolMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveAsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem LoadDataMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel TickCountStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar SimulationProgress;
		private System.Windows.Forms.ToolStripMenuItem StopLoadingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem StopSimulationMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel StateStatusLabel;
		private System.Windows.Forms.ToolStrip traderToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox DataSourceComboBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripComboBox TickProviderComboBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripTextBox MaxTicksTextBox;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.Panel ResultsPanel;
		private System.Windows.Forms.Panel PropertiesPanel;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox StrategiesComboBox;
		public System.Windows.Forms.PropertyGrid StrategySettings;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox SymbolComboBox;
		public System.Windows.Forms.PropertyGrid SymbolProperties;
		private System.Windows.Forms.ToolStripButton RunSimulationButton;
		private System.Windows.Forms.ToolStripButton StopSimulationButton;
		private SingleSimulation SingleSimulationControl;
		private System.Windows.Forms.GroupBox groupBox3;
		public System.Windows.Forms.PropertyGrid SimulationAnalysis;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripTextBox SimulationIterations;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem SaveResultsMenuItem;
	}
}

