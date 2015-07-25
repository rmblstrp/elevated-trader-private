﻿namespace ElevatedTrader.Windows.Forms
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, "25,18,20,22");
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, "26,20,22,25");
			System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 21.35D);
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 24.75D);
			System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
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
			this.SaveSymbolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ShowGraphMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TradesBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.MaxTickTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.RunSimulationButton = new System.Windows.Forms.ToolStripButton();
			this.StopSimulationButton = new System.Windows.Forms.ToolStripButton();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.ResultsPanel = new System.Windows.Forms.Panel();
			this.TradeInfoSplitContainer = new System.Windows.Forms.SplitContainer();
			this.TradeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.TradeResultGrid = new System.Windows.Forms.DataGridView();
			this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.equityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.profitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PropertiesPanel = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.StrategiesComboBox = new System.Windows.Forms.ComboBox();
			this.StrategySettings = new System.Windows.Forms.PropertyGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SymbolComboBox = new System.Windows.Forms.ComboBox();
			this.SymbolProperties = new System.Windows.Forms.PropertyGrid();
			this.MainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TradesBindingSource)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.traderToolStrip.SuspendLayout();
			this.MainPanel.SuspendLayout();
			this.ResultsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TradeInfoSplitContainer)).BeginInit();
			this.TradeInfoSplitContainer.Panel1.SuspendLayout();
			this.TradeInfoSplitContainer.Panel2.SuspendLayout();
			this.TradeInfoSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TradeChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeResultGrid)).BeginInit();
			this.PropertiesPanel.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.BackColor = System.Drawing.SystemColors.MenuBar;
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.toolStripMenuItem1,
            this.SimulationMenuItem,
            this.symbolsToolStripMenuItem,
            this.viewToolStripMenuItem});
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
			this.NewSolutionMenuItem.Size = new System.Drawing.Size(145, 22);
			this.NewSolutionMenuItem.Text = "New Solution";
			// 
			// OpenMenuItem
			// 
			this.OpenMenuItem.Name = "OpenMenuItem";
			this.OpenMenuItem.Size = new System.Drawing.Size(145, 22);
			this.OpenMenuItem.Text = "Open...";
			this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
			// 
			// SaveMenuItem
			// 
			this.SaveMenuItem.Name = "SaveMenuItem";
			this.SaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.SaveMenuItem.Size = new System.Drawing.Size(145, 22);
			this.SaveMenuItem.Text = "Save";
			this.SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
			// 
			// SaveAsMenuItem
			// 
			this.SaveAsMenuItem.Name = "SaveAsMenuItem";
			this.SaveAsMenuItem.Size = new System.Drawing.Size(145, 22);
			this.SaveAsMenuItem.Text = "Save As...";
			this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
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
			this.LoadDataMenuItem.Size = new System.Drawing.Size(152, 22);
			this.LoadDataMenuItem.Text = "Load";
			this.LoadDataMenuItem.Click += new System.EventHandler(this.LoadDataMenuItem_Click);
			// 
			// StopLoadingMenuItem
			// 
			this.StopLoadingMenuItem.Name = "StopLoadingMenuItem";
			this.StopLoadingMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.StopLoadingMenuItem.Size = new System.Drawing.Size(152, 22);
			this.StopLoadingMenuItem.Text = "Stop";
			this.StopLoadingMenuItem.Click += new System.EventHandler(this.StopLoadingMenuItem_Click);
			// 
			// SimulationMenuItem
			// 
			this.SimulationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunSimulationMenuItem,
            this.StopSimulationMenuItem});
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
			this.RunSimulationMenuItem.Click += new System.EventHandler(this.RunSimulationMenuItem_Click);
			// 
			// StopSimulationMenuItem
			// 
			this.StopSimulationMenuItem.Name = "StopSimulationMenuItem";
			this.StopSimulationMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.StopSimulationMenuItem.Size = new System.Drawing.Size(152, 22);
			this.StopSimulationMenuItem.Text = "Stop";
			this.StopSimulationMenuItem.Click += new System.EventHandler(this.StopSimulationMenuItem_Click);
			// 
			// symbolsToolStripMenuItem
			// 
			this.symbolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSymbolMenuItem,
            this.SaveSymbolMenuItem});
			this.symbolsToolStripMenuItem.Name = "symbolsToolStripMenuItem";
			this.symbolsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.symbolsToolStripMenuItem.Text = "Symbols";
			// 
			// AddSymbolMenuItem
			// 
			this.AddSymbolMenuItem.Name = "AddSymbolMenuItem";
			this.AddSymbolMenuItem.Size = new System.Drawing.Size(217, 22);
			this.AddSymbolMenuItem.Text = "Add Symbol";
			this.AddSymbolMenuItem.Click += new System.EventHandler(this.AddSymbolMenuItem_Click);
			// 
			// SaveSymbolMenuItem
			// 
			this.SaveSymbolMenuItem.Name = "SaveSymbolMenuItem";
			this.SaveSymbolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.SaveSymbolMenuItem.Size = new System.Drawing.Size(217, 22);
			this.SaveSymbolMenuItem.Text = "Save Selected";
			this.SaveSymbolMenuItem.Click += new System.EventHandler(this.SaveSymbolMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowGraphMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// ShowGraphMenuItem
			// 
			this.ShowGraphMenuItem.Name = "ShowGraphMenuItem";
			this.ShowGraphMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.ShowGraphMenuItem.Size = new System.Drawing.Size(180, 22);
			this.ShowGraphMenuItem.Text = "Show Graph";
			this.ShowGraphMenuItem.Click += new System.EventHandler(this.ShowGraphMenuItem_Click);
			// 
			// TradesBindingSource
			// 
			this.TradesBindingSource.DataSource = typeof(ElevatedTrader.ITrade);
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
            this.toolStripLabel3,
            this.MaxTickTextBox,
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
			// toolStripLabel3
			// 
			this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(80, 22);
			this.toolStripLabel3.Text = "Max Records: ";
			// 
			// MaxTickTextBox
			// 
			this.MaxTickTextBox.Name = "MaxTickTextBox";
			this.MaxTickTextBox.Size = new System.Drawing.Size(100, 25);
			this.MaxTickTextBox.TextChanged += new System.EventHandler(this.MaxTickTextBox_TextChanged);
			// 
			// RunSimulationButton
			// 
			this.RunSimulationButton.Image = global::ElevatedTrader.Windows.Forms.ToolstripResources.control_play;
			this.RunSimulationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RunSimulationButton.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.RunSimulationButton.Name = "RunSimulationButton";
			this.RunSimulationButton.Size = new System.Drawing.Size(108, 22);
			this.RunSimulationButton.Text = "Run Simulation";
			this.RunSimulationButton.Click += new System.EventHandler(this.RunSimulationButton_Click);
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
			this.StopSimulationButton.Click += new System.EventHandler(this.StopSimulationButton_Click);
			// 
			// MainPanel
			// 
			this.MainPanel.Controls.Add(this.ResultsPanel);
			this.MainPanel.Controls.Add(this.PropertiesPanel);
			this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainPanel.Location = new System.Drawing.Point(0, 49);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(1183, 628);
			this.MainPanel.TabIndex = 7;
			// 
			// ResultsPanel
			// 
			this.ResultsPanel.Controls.Add(this.TradeInfoSplitContainer);
			this.ResultsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResultsPanel.Location = new System.Drawing.Point(0, 0);
			this.ResultsPanel.Name = "ResultsPanel";
			this.ResultsPanel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
			this.ResultsPanel.Size = new System.Drawing.Size(933, 628);
			this.ResultsPanel.TabIndex = 4;
			// 
			// TradeInfoSplitContainer
			// 
			this.TradeInfoSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradeInfoSplitContainer.Location = new System.Drawing.Point(5, 0);
			this.TradeInfoSplitContainer.Name = "TradeInfoSplitContainer";
			this.TradeInfoSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// TradeInfoSplitContainer.Panel1
			// 
			this.TradeInfoSplitContainer.Panel1.BackColor = System.Drawing.Color.Black;
			this.TradeInfoSplitContainer.Panel1.Controls.Add(this.TradeChart);
			// 
			// TradeInfoSplitContainer.Panel2
			// 
			this.TradeInfoSplitContainer.Panel2.Controls.Add(this.TradeResultGrid);
			this.TradeInfoSplitContainer.Size = new System.Drawing.Size(923, 623);
			this.TradeInfoSplitContainer.SplitterDistance = 312;
			this.TradeInfoSplitContainer.TabIndex = 1;
			// 
			// TradeChart
			// 
			this.TradeChart.BackColor = System.Drawing.Color.Black;
			this.TradeChart.BorderlineColor = System.Drawing.Color.Gray;
			this.TradeChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
			chartArea2.AxisX.LineColor = System.Drawing.Color.Gainsboro;
			chartArea2.AxisX.MajorGrid.Enabled = false;
			chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
			chartArea2.AxisX.Minimum = 0D;
			chartArea2.AxisY.IsStartedFromZero = false;
			chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
			chartArea2.AxisY.LineColor = System.Drawing.Color.Gainsboro;
			chartArea2.AxisY.MajorGrid.Enabled = false;
			chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
			chartArea2.AxisY2.IsStartedFromZero = false;
			chartArea2.BackColor = System.Drawing.Color.Black;
			chartArea2.BorderColor = System.Drawing.Color.Gainsboro;
			chartArea2.Name = "TradeChart";
			this.TradeChart.ChartAreas.Add(chartArea2);
			this.TradeChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradeChart.Location = new System.Drawing.Point(0, 0);
			this.TradeChart.Name = "TradeChart";
			this.TradeChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			series3.ChartArea = "TradeChart";
			series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Stock;
			series3.Color = System.Drawing.Color.DimGray;
			series3.Name = "Series1";
			series3.Points.Add(dataPoint5);
			series3.Points.Add(dataPoint6);
			series3.YValuesPerPoint = 4;
			series4.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
			series4.ChartArea = "TradeChart";
			series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
			series4.Color = System.Drawing.Color.WhiteSmoke;
			series4.Name = "Series2";
			series4.Points.Add(dataPoint7);
			series4.Points.Add(dataPoint8);
			series4.ShadowColor = System.Drawing.Color.Empty;
			this.TradeChart.Series.Add(series3);
			this.TradeChart.Series.Add(series4);
			this.TradeChart.Size = new System.Drawing.Size(923, 312);
			this.TradeChart.TabIndex = 3;
			this.TradeChart.Text = "chart1";
			title2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			title2.ForeColor = System.Drawing.Color.White;
			title2.Name = "Title1";
			title2.Text = "Trade Results";
			this.TradeChart.Titles.Add(title2);
			// 
			// TradeResultGrid
			// 
			this.TradeResultGrid.AutoGenerateColumns = false;
			this.TradeResultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.TradeResultGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeDataGridViewTextBoxColumn,
            this.quantityDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.equityDataGridViewTextBoxColumn,
            this.profitDataGridViewTextBoxColumn});
			this.TradeResultGrid.DataSource = this.TradesBindingSource;
			this.TradeResultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradeResultGrid.Location = new System.Drawing.Point(0, 0);
			this.TradeResultGrid.MultiSelect = false;
			this.TradeResultGrid.Name = "TradeResultGrid";
			this.TradeResultGrid.ReadOnly = true;
			this.TradeResultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.TradeResultGrid.Size = new System.Drawing.Size(923, 307);
			this.TradeResultGrid.TabIndex = 3;
			// 
			// typeDataGridViewTextBoxColumn
			// 
			this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
			this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
			this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
			this.typeDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// quantityDataGridViewTextBoxColumn
			// 
			this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
			dataGridViewCellStyle5.Format = "N0";
			dataGridViewCellStyle5.NullValue = null;
			this.quantityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
			this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
			this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
			this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// priceDataGridViewTextBoxColumn
			// 
			this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
			dataGridViewCellStyle6.Format = "C2";
			dataGridViewCellStyle6.NullValue = null;
			this.priceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
			this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
			this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
			this.priceDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// equityDataGridViewTextBoxColumn
			// 
			this.equityDataGridViewTextBoxColumn.DataPropertyName = "Equity";
			dataGridViewCellStyle7.Format = "C2";
			dataGridViewCellStyle7.NullValue = null;
			this.equityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
			this.equityDataGridViewTextBoxColumn.HeaderText = "Equity";
			this.equityDataGridViewTextBoxColumn.Name = "equityDataGridViewTextBoxColumn";
			this.equityDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// profitDataGridViewTextBoxColumn
			// 
			this.profitDataGridViewTextBoxColumn.DataPropertyName = "Profit";
			dataGridViewCellStyle8.Format = "C2";
			dataGridViewCellStyle8.NullValue = null;
			this.profitDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
			this.profitDataGridViewTextBoxColumn.HeaderText = "Profit";
			this.profitDataGridViewTextBoxColumn.Name = "profitDataGridViewTextBoxColumn";
			this.profitDataGridViewTextBoxColumn.ReadOnly = true;
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
			this.SymbolComboBox.DataSource = this.TradesBindingSource;
			this.SymbolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SymbolComboBox.FormattingEnabled = true;
			this.SymbolComboBox.Location = new System.Drawing.Point(6, 20);
			this.SymbolComboBox.Name = "SymbolComboBox";
			this.SymbolComboBox.Size = new System.Drawing.Size(231, 21);
			this.SymbolComboBox.TabIndex = 1;
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
			((System.ComponentModel.ISupportInitialize)(this.TradesBindingSource)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.traderToolStrip.ResumeLayout(false);
			this.traderToolStrip.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.ResultsPanel.ResumeLayout(false);
			this.TradeInfoSplitContainer.Panel1.ResumeLayout(false);
			this.TradeInfoSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TradeInfoSplitContainer)).EndInit();
			this.TradeInfoSplitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TradeChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeResultGrid)).EndInit();
			this.PropertiesPanel.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveMenuItem;
		private System.Windows.Forms.BindingSource TradesBindingSource;
		private System.Windows.Forms.ToolStripMenuItem SimulationMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RunSimulationMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem NewSolutionMenuItem;
		private System.Windows.Forms.ToolStripMenuItem symbolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AddSymbolMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveSymbolMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveAsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem LoadDataMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel TickCountStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar SimulationProgress;
		private System.Windows.Forms.ToolStripMenuItem StopLoadingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem StopSimulationMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel StateStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ShowGraphMenuItem;
		private System.Windows.Forms.ToolStrip traderToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox DataSourceComboBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripComboBox TickProviderComboBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripTextBox MaxTickTextBox;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.Panel ResultsPanel;
		private System.Windows.Forms.SplitContainer TradeInfoSplitContainer;
		private System.Windows.Forms.DataVisualization.Charting.Chart TradeChart;
		private System.Windows.Forms.DataGridView TradeResultGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn equityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn profitDataGridViewTextBoxColumn;
		private System.Windows.Forms.Panel PropertiesPanel;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox StrategiesComboBox;
		public System.Windows.Forms.PropertyGrid StrategySettings;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox SymbolComboBox;
		public System.Windows.Forms.PropertyGrid SymbolProperties;
		private System.Windows.Forms.ToolStripButton RunSimulationButton;
		private System.Windows.Forms.ToolStripButton StopSimulationButton;
	}
}

