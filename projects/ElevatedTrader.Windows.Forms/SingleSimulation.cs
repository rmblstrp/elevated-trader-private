using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ElevatedTrader.Windows.Forms
{
	public partial class SingleSimulation : UserControl
	{
		private BindingList<ITradeEntry> trades;
		private List<ITradeEntry> tradeBuffer = new List<ITradeEntry>(10000);
		private Dictionary<int, Series> periodSeries = new Dictionary<int, Series>();
		private Dictionary<int, Series> tradeSeries = new Dictionary<int, Series>();
		private Dictionary<IIndicator, Series> indicatorSeries = new Dictionary<IIndicator, Series>();
		private SessionAnalyzer analyzer = new SessionAnalyzer();

		public event EventHandler<int> Tick;

		public ISessionAnalyzer Analyzer
		{
			get { return analyzer; }
		}

		public SingleSimulation()
		{
			InitializeComponent();
		}

		public async Task RunSimulation(ITradingStrategy strategy, ITickProvider provider, int tickCount)
		{
			try
			{
				TradeChart.Series.Clear();
				periodSeries.Clear();
				tradeSeries.Clear();
				indicatorSeries.Clear();
				tradeBuffer.Clear();

				TradeResultGrid.Columns[2].DefaultCellStyle.Format = "C" + (strategy.Session.Instrument.TickRate.ToString().Length - 2);

				if (trades != null)
				{
					trades.Clear();
					TradesBindingSource.DataSource = null;
				}

				TradeChart.Visible = false;

				var runner = new TradingStrategyRunner();
				runner.Tick += (obj, index) => { if (Tick != null) Tick(obj, index); };

				await Task.Run(() => { runner.Run(strategy, provider, tickCount); });

				trades = new BindingList<ITradeEntry>(strategy.Session.Trades.ToList());
				TradesBindingSource.DataSource = trades;

				PopulateTickSeries(strategy);

				PopulateIndicatorSeries(strategy);

				PopulateTradeSeries(strategy);

				analyzer.Analyze(strategy.Session);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				strategy.Clear();

				GC.Collect();
				GC.WaitForPendingFinalizers();

				TradeChart.Visible = true;
			}
		}

		#region -- Chart Setup --
		private void PopulateTradeSeries(ITradingStrategy strategy)
		{
			var ts = CreateTradeSeries();

			foreach (var kv in periodSeries)
			{
				tradeSeries.Add(kv.Key, ts);
				TradeChart.Series.Add(ts);

				for (int index = 0; index < strategy.Session.Trades.Count; index++)
				{
					tradeSeries[kv.Key].Points.Add(CreateTradeDataPoint(kv.Key, strategy.Session.Trades[index]));
				}
			}
		}

		private void PopulateIndicatorSeries(ITradingStrategy strategy)
		{
			foreach (var size in strategy.Indicators.Keys)
			{
				var item = strategy.Indicators[size];

				for (int index = 0; index < item.Count; index++)
				{
					var series = CreateIndicatorSeries();

					var indicator = item[index];

					for (int idx = 0; idx < indicator.Results.Count; idx++)
					{
						var point = CreateIndicatorDataPoint(idx, indicator.Results[idx]);

						if (point != null)
						{
							series.Points.Add(point);
						}
					}

					indicatorSeries.Add(indicator, series);

					TradeChart.Series.Add(series);
				}
			}
		}

		private void PopulateTickSeries(ITradingStrategy strategy)
		{
			foreach (var size in strategy.Aggregator.Periods.Keys)
			{
				var series = CreatePeriodSeries();

				var item = strategy.Aggregator.Periods[size];

				for (var index = 0; index < item.Count; index++)
				{
					series.Points.Add(CreatePeriodDataPoint(index, item[index]));
				}

				periodSeries.Add(size, series);

				TradeChart.Series.Add(series);
			}
		}

		private Series CreatePeriodSeries()
		{
			var item = new Series()
			{
				ChartArea = "TradeChart",
				ChartType = SeriesChartType.Stock,
				IsXValueIndexed = false,
				YValuesPerPoint = 4,
				Color = Color.DimGray
			};

			return item;
		}

		private Series CreateTradeSeries()
		{
			var item = new Series()
			{
				ChartArea = "TradeChart",
				ChartType = SeriesChartType.Point,
				IsXValueIndexed = false,
				YValuesPerPoint = 1,
				Color = Color.Gold
			};

			return item;
		}

		private Series CreateIndicatorSeries()
		{
			var item = new Series()
			{
				ChartArea = "TradeChart",
				ChartType = SeriesChartType.Line,
				IsXValueIndexed = false,
				YValuesPerPoint = 1,
				Palette = ChartColorPalette.Pastel,
				YAxisType = AxisType.Primary
			};

			return item;
		}

		private DataPoint CreatePeriodDataPoint(int index, ITradingPeriod period)
		{
			var item = new DataPoint()
			{
				XValue = index + 1,
				YValues = new double[] { period.High, period.Low, period.Open, period.Close }
			};

			return item;
		}

		private DataPoint CreateTradeDataPoint(int size, ITradeEntry trade)
		{
			var item = new DataPoint()
			{
				XValue = trade.Indexes[size] + 1,
				YValues = new double[] { trade.Price },
				Color = trade.Type == TradeType.Buy ? Color.LightGreen : Color.LightPink
			};

			return item;
		}

		private DataPoint CreateIndicatorDataPoint(int index, IIndicatorResult result)
		{
			if (result.Values.Count == 0)
			{
				return null;
			}

			var item = new DataPoint()
			{
				XValue = index + 1,
				YValues = new double[] { result.Values[0] },
				Color = result.Direction == TrendDirection.Rising ? Color.LightGreen : Color.LightPink
			};

			return item;
		}
		#endregion
	}
}
