namespace ElevatedTrader.Windows.Forms
{
	partial class SingleSimulation
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, "25,18,20,22");
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, "26,20,22,25");
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 21.35D);
			System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 24.75D);
			System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.TradeInfoSplitContainer = new System.Windows.Forms.SplitContainer();
			this.TradeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.TradeResultGrid = new System.Windows.Forms.DataGridView();
			this.TradesBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.equityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.profitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.TradeInfoSplitContainer)).BeginInit();
			this.TradeInfoSplitContainer.Panel1.SuspendLayout();
			this.TradeInfoSplitContainer.Panel2.SuspendLayout();
			this.TradeInfoSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TradeChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeResultGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradesBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// TradeInfoSplitContainer
			// 
			this.TradeInfoSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradeInfoSplitContainer.Location = new System.Drawing.Point(0, 0);
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
			this.TradeInfoSplitContainer.Size = new System.Drawing.Size(889, 633);
			this.TradeInfoSplitContainer.SplitterDistance = 317;
			this.TradeInfoSplitContainer.TabIndex = 2;
			// 
			// TradeChart
			// 
			this.TradeChart.BackColor = System.Drawing.Color.Black;
			this.TradeChart.BorderlineColor = System.Drawing.Color.Gray;
			this.TradeChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
			chartArea1.AxisX.LineColor = System.Drawing.Color.Gainsboro;
			chartArea1.AxisX.MajorGrid.Enabled = false;
			chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
			chartArea1.AxisX.Minimum = 0D;
			chartArea1.AxisY.IsStartedFromZero = false;
			chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
			chartArea1.AxisY.LineColor = System.Drawing.Color.Gainsboro;
			chartArea1.AxisY.MajorGrid.Enabled = false;
			chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
			chartArea1.AxisY2.IsStartedFromZero = false;
			chartArea1.BackColor = System.Drawing.Color.Black;
			chartArea1.BorderColor = System.Drawing.Color.Gainsboro;
			chartArea1.Name = "TradeChart";
			this.TradeChart.ChartAreas.Add(chartArea1);
			this.TradeChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradeChart.Location = new System.Drawing.Point(0, 0);
			this.TradeChart.Name = "TradeChart";
			this.TradeChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			series1.ChartArea = "TradeChart";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Stock;
			series1.Color = System.Drawing.Color.DimGray;
			series1.Name = "Series1";
			series1.Points.Add(dataPoint1);
			series1.Points.Add(dataPoint2);
			series1.YValuesPerPoint = 4;
			series2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
			series2.ChartArea = "TradeChart";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
			series2.Color = System.Drawing.Color.WhiteSmoke;
			series2.Name = "Series2";
			series2.Points.Add(dataPoint3);
			series2.Points.Add(dataPoint4);
			series2.ShadowColor = System.Drawing.Color.Empty;
			this.TradeChart.Series.Add(series1);
			this.TradeChart.Series.Add(series2);
			this.TradeChart.Size = new System.Drawing.Size(889, 317);
			this.TradeChart.TabIndex = 3;
			this.TradeChart.Text = "chart1";
			title1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			title1.ForeColor = System.Drawing.Color.White;
			title1.Name = "Title1";
			title1.Text = "Trade Results";
			this.TradeChart.Titles.Add(title1);
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
			this.TradeResultGrid.Size = new System.Drawing.Size(889, 312);
			this.TradeResultGrid.TabIndex = 3;
			// 
			// TradesBindingSource
			// 
			this.TradesBindingSource.DataSource = typeof(ElevatedTrader.ITrade);
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
			dataGridViewCellStyle1.Format = "N0";
			this.quantityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
			this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
			this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// priceDataGridViewTextBoxColumn
			// 
			this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
			dataGridViewCellStyle2.Format = "C4";
			dataGridViewCellStyle2.NullValue = null;
			this.priceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
			this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
			this.priceDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// equityDataGridViewTextBoxColumn
			// 
			this.equityDataGridViewTextBoxColumn.DataPropertyName = "Equity";
			dataGridViewCellStyle3.Format = "C2";
			dataGridViewCellStyle3.NullValue = null;
			this.equityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.equityDataGridViewTextBoxColumn.HeaderText = "Equity";
			this.equityDataGridViewTextBoxColumn.Name = "equityDataGridViewTextBoxColumn";
			this.equityDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// profitDataGridViewTextBoxColumn
			// 
			this.profitDataGridViewTextBoxColumn.DataPropertyName = "Profit";
			dataGridViewCellStyle4.Format = "C2";
			this.profitDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.profitDataGridViewTextBoxColumn.HeaderText = "Profit";
			this.profitDataGridViewTextBoxColumn.Name = "profitDataGridViewTextBoxColumn";
			this.profitDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// SingleSimulation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TradeInfoSplitContainer);
			this.Name = "SingleSimulation";
			this.Size = new System.Drawing.Size(889, 633);
			this.TradeInfoSplitContainer.Panel1.ResumeLayout(false);
			this.TradeInfoSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TradeInfoSplitContainer)).EndInit();
			this.TradeInfoSplitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TradeChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeResultGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradesBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer TradeInfoSplitContainer;
		private System.Windows.Forms.DataVisualization.Charting.Chart TradeChart;
		private System.Windows.Forms.DataGridView TradeResultGrid;
		private System.Windows.Forms.BindingSource TradesBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn equityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn profitDataGridViewTextBoxColumn;
	}
}
