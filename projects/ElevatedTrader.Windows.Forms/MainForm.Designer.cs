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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TradeResultGrid = new System.Windows.Forms.DataGridView();
			this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.equityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.profitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.iTradeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SymbolProperties = new System.Windows.Forms.PropertyGrid();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.StrategySettings = new System.Windows.Forms.PropertyGrid();
			this.StrategiesMenuItem = new System.Windows.Forms.ToolStripComboBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TradeResultGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iTradeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.simulationToolStripMenuItem,
            this.StrategiesMenuItem});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(846, 27);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingsToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openToolStripMenuItem.Text = "Open";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// simulationToolStripMenuItem
			// 
			this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem});
			this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
			this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.simulationToolStripMenuItem.Text = "Simulation";
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
			this.runToolStripMenuItem.Text = "Run";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 27);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.TradeResultGrid);
			this.splitContainer1.Panel1MinSize = 550;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(846, 445);
			this.splitContainer1.SplitterDistance = 550;
			this.splitContainer1.TabIndex = 1;
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
			this.TradeResultGrid.DataSource = this.iTradeBindingSource;
			this.TradeResultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradeResultGrid.Location = new System.Drawing.Point(0, 0);
			this.TradeResultGrid.MultiSelect = false;
			this.TradeResultGrid.Name = "TradeResultGrid";
			this.TradeResultGrid.ReadOnly = true;
			this.TradeResultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.TradeResultGrid.Size = new System.Drawing.Size(550, 445);
			this.TradeResultGrid.TabIndex = 0;
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
			dataGridViewCellStyle21.Format = "N0";
			dataGridViewCellStyle21.NullValue = null;
			this.quantityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle21;
			this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
			this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
			this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// priceDataGridViewTextBoxColumn
			// 
			this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
			dataGridViewCellStyle22.Format = "C2";
			dataGridViewCellStyle22.NullValue = null;
			this.priceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle22;
			this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
			this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
			this.priceDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// equityDataGridViewTextBoxColumn
			// 
			this.equityDataGridViewTextBoxColumn.DataPropertyName = "Equity";
			dataGridViewCellStyle23.Format = "C2";
			dataGridViewCellStyle23.NullValue = null;
			this.equityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle23;
			this.equityDataGridViewTextBoxColumn.HeaderText = "Equity";
			this.equityDataGridViewTextBoxColumn.Name = "equityDataGridViewTextBoxColumn";
			this.equityDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// profitDataGridViewTextBoxColumn
			// 
			this.profitDataGridViewTextBoxColumn.DataPropertyName = "Profit";
			dataGridViewCellStyle24.Format = "C2";
			dataGridViewCellStyle24.NullValue = null;
			this.profitDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle24;
			this.profitDataGridViewTextBoxColumn.HeaderText = "Profit";
			this.profitDataGridViewTextBoxColumn.Name = "profitDataGridViewTextBoxColumn";
			this.profitDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// iTradeBindingSource
			// 
			this.iTradeBindingSource.DataSource = typeof(ElevatedTrader.ITrade);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer2.Size = new System.Drawing.Size(292, 445);
			this.splitContainer2.SplitterDistance = 176;
			this.splitContainer2.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.SymbolProperties);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(292, 176);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Symbol Properties";
			// 
			// SymbolProperties
			// 
			this.SymbolProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SymbolProperties.HelpVisible = false;
			this.SymbolProperties.Location = new System.Drawing.Point(3, 16);
			this.SymbolProperties.Name = "SymbolProperties";
			this.SymbolProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.SymbolProperties.Size = new System.Drawing.Size(286, 157);
			this.SymbolProperties.TabIndex = 0;
			this.SymbolProperties.ToolbarVisible = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.StrategySettings);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(292, 265);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Strategy Settings";
			// 
			// StrategySettings
			// 
			this.StrategySettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.StrategySettings.HelpVisible = false;
			this.StrategySettings.Location = new System.Drawing.Point(3, 16);
			this.StrategySettings.Name = "StrategySettings";
			this.StrategySettings.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.StrategySettings.Size = new System.Drawing.Size(286, 246);
			this.StrategySettings.TabIndex = 0;
			this.StrategySettings.ToolbarVisible = false;
			// 
			// StrategiesMenuItem
			// 
			this.StrategiesMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.StrategiesMenuItem.Name = "StrategiesMenuItem";
			this.StrategiesMenuItem.Size = new System.Drawing.Size(150, 23);
			this.StrategiesMenuItem.Sorted = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 472);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(846, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 23);
			this.toolStripMenuItem1.Text = "Data";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.settingsToolStripMenuItem.Text = "Settings";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(846, 494);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.MainMenu);
			this.Controls.Add(this.statusStrip1);
			this.MainMenuStrip = this.MainMenu;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Elevated Trader";
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TradeResultGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iTradeBindingSource)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.DataGridView TradeResultGrid;
		private System.Windows.Forms.BindingSource iTradeBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn equityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn profitDataGridViewTextBoxColumn;
		private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		public System.Windows.Forms.PropertyGrid SymbolProperties;
		public System.Windows.Forms.PropertyGrid StrategySettings;
		private System.Windows.Forms.ToolStripComboBox StrategiesMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
	}
}

