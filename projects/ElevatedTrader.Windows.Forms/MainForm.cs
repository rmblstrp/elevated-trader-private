namespace ElevatedTrader.Windows.Forms
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Data;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Dynamic;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using CSScriptLibrary;
	using Newtonsoft.Json;
	using System.Windows.Forms.DataVisualization.Charting;
	using MathNet.Numerics.Distributions;

	public partial class MainForm : Form
	{
		#region -- Internal Classes --
		private enum ApplicationState
		{
			Idle,
			Loading,
			Running
		}
		#endregion

		#region -- Private Fields --
		private string filename = null;
		private bool busy = false;
		private TraderSettings settings;
		// "Data Source=localhost\sqlexpress;Initial Catalog=AutomatedTradingLive;Integrated Security=True"

		private BindingSource symbolsBindingSource = new BindingSource();
		private BindingList<TradeSymbol> symbols = new BindingList<TradeSymbol>();
		private TradeSymbol symbol;

		private BindingSource strategyBindingSource = new BindingSource();
		private BindingList<string> strategies = new BindingList<string>();
		private ITradingStrategy strategy;

		private SolutionSettings solution = new SolutionSettings();
		private FileSystemWatcher indicatorsWatcher;
		private FileSystemWatcher strategiesWatcher;

		private OpenFileDialog openDialog;
		private SaveFileDialog saveDialog;

		private Assembly scripts_assembly = null;
		private string[] reference_assemblies = new string[]
		{
			"ElevatedTrader",
			"ElevatedTrader.Math",
			"MathNet.Numerics",
			"MathNet.Filtering",
			"MathNet.Filtering.Kalman",
			"Accord",
			"Accord.Extensions.Core",
			"Accord.Extensions.Math",
			"Accord.Extensions.Statistics",
			"Accord.Math",
			"Accord.Statistics",
			"AForge",
			"AForge.Math"
		};

		class HistoryContainer
		{
			public TradeHistoryType Type;
			public object Item;
		}

		private class HistoryList
		{
			private List<TickDelta> differences = new List<TickDelta>(50000000);

			public int TickCount
			{
				get;
				set;
			}

			public List<TickDelta> Differences
			{
				get { return differences; }
			}

			public ITradeTick Tick
			{
				get;
				set;
			}

			public long MaxId
			{
				get;
				set;
			}
		}

		private Dictionary<string, HistoryList> history = new Dictionary<string, HistoryList>(10);

		private BindingList<ITrade> trades;
		private List<ITrade> tradeBuffer = new List<ITrade>(10000);
		private Dictionary<int, Series> periodSeries = new Dictionary<int, Series>();
		private Dictionary<int, Series> tradeSeries = new Dictionary<int, Series>();
		private Dictionary<IIndicator, Series> indicatorSeries = new Dictionary<IIndicator, Series>();
		#endregion

		#region -- Constants --
		const string PathBase = @"library\";
		const string SymbolsPath = @"symbols\";
		const string IndicatorsPath = PathBase + @"indicators\";
		const string StrategiesPath = PathBase + @"strategies\";
		const string SolutionsPath = PathBase + @"solutions\";

		const string ScriptsFilter = "*.cs";

		const string SymbolsExtension = ".json";
		const string SymbolsFilter = "*" + SymbolsExtension;

		const string SolutionExtension = ".json";
		const string SolutionFilter = "JSON Solution|*" + SolutionExtension;
		#endregion

		public MainForm()
		{
			InitializeComponent();

			settings = TraderSettings.Load();

			LoadScripts();

			indicatorsWatcher = new FileSystemWatcher(IndicatorsPath, ScriptsFilter)
			{
				NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
			};
			strategiesWatcher = new FileSystemWatcher(StrategiesPath, ScriptsFilter)
			{
				NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
			};

			indicatorsWatcher.Changed += indicatorsWatcher_Changed;
			indicatorsWatcher.Created += indicatorsWatcher_Changed;
			indicatorsWatcher.Deleted += indicatorsWatcher_Changed;
			indicatorsWatcher.Renamed += indicatorsWatcher_Changed;

			strategiesWatcher.Changed += strategiesWatcher_Changed;
			strategiesWatcher.Created += strategiesWatcher_Changed;
			strategiesWatcher.Deleted += strategiesWatcher_Changed;
			strategiesWatcher.Renamed += strategiesWatcher_Changed;

			indicatorsWatcher.EnableRaisingEvents = true;
			strategiesWatcher.EnableRaisingEvents = true;

			strategyBindingSource.DataSource = strategies;
			StrategiesComboBox.DataSource = strategyBindingSource;

			LoadSymbols();

			openDialog = new OpenFileDialog()
			{
				DefaultExt = SolutionExtension,
				FileName = string.Empty,
				Filter = SolutionFilter,
				InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + SolutionsPath
			};

			saveDialog = new SaveFileDialog()
			{
				DefaultExt = SolutionExtension,
				FileName = string.Empty,
				Filter = SolutionFilter,
				InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + SolutionsPath
			};

			StrategiesComboBox.DataSource = strategies;
			SetShowGraphCheckedState();

			generateTicksMenuItem.Checked = settings.GenerateTickData;
			ShuffleLoadedDataMenuItem.Checked = settings.ShuffleGeneratedTickData;

			FormClosing += delegate { TraderSettings.Save(settings); };

			MathNet.Numerics.Control.UseMultiThreading();
			MathNet.Numerics.Control.UseNativeMKL();
		}

		#region -- Strategies --
		int indicator_notify_count = 0;
		void indicatorsWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			Action a = () =>
			{
				LoadScripts();
			};

			if (++indicator_notify_count == 3)
			{
				this.Invoke(a);
				indicator_notify_count = 0;
			}
		}

		int strategy_notify_count = 0;
		void strategiesWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			Action a = () =>
			{
				LoadScripts();
			};

			if (++strategy_notify_count == 3)
			{
				this.Invoke(a);
				strategy_notify_count = 0;
			}
		}

		private void LoadScripts()
		{
			var files = ListStrategyFiles().Union(ListIndicatorsFiles()).ToArray();

			scripts_assembly = Assembly.LoadFile(CSScript.CompileFiles(files, null, true, reference_assemblies));

			var implements = typeof(ITradingStrategy);
			var types = scripts_assembly.GetTypes().Where(t => implements.IsAssignableFrom(t));

			var selected = (string)StrategiesComboBox.SelectedItem;
			var settings = strategy == null ? null : strategy.Settings;

			strategies.Clear();

			foreach (var item in types)
			{
				strategies.Add(item.FullName);
			}

			if (!string.IsNullOrWhiteSpace(selected))
			{
				var index = strategies.IndexOf(selected);
				StrategiesComboBox.SelectedIndex = -1;
				StrategiesComboBox.SelectedIndex = index;
				strategy.Settings = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(settings));
			}
		}

		private IEnumerable<string> ListIndicatorsFiles()
		{
			return Directory.EnumerateFiles(IndicatorsPath, ScriptsFilter, SearchOption.TopDirectoryOnly);
		}

		private IEnumerable<string> ListStrategyFiles()
		{
			return Directory.EnumerateFiles(StrategiesPath, ScriptsFilter, SearchOption.TopDirectoryOnly);
		}

		private void InitializeStrategy(string name)
		{
			if (strategy != null)
			{
				// should probably do other cleanup here;
				strategy = null;
			}

			strategy = (ITradingStrategy)scripts_assembly.CreateInstance(name);
			StrategySettings.SelectedObject = strategy.Settings;
			strategy.Session.Symbol = symbol;

			solution.Strategy = name;
			solution.Settings = null;
		}

		private void StrategiesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (StrategiesComboBox.SelectedIndex < 0) return;

			InitializeStrategy(StrategiesComboBox.Text);
		}
		#endregion

		#region -- Symbols --
		private void LoadSymbols()
		{
			var files = Directory.EnumerateFiles(SymbolsPath, SymbolsFilter, SearchOption.TopDirectoryOnly);

			foreach (var file in files)
			{
				var item = JsonConvert.DeserializeObject<TradeSymbol>(File.ReadAllText(file));

				symbols.Add(item);
			}

			//symbols.Sort((a, b) => a.Symbol.CompareTo(b.Symbol));
			symbolsBindingSource.DataSource = symbols;
			SymbolComboBox.DataSource = symbolsBindingSource;
		}

		private void SymbolComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			symbol = symbols[SymbolComboBox.SelectedIndex];
			SymbolProperties.SelectedObject = symbol;

			solution.Symbol = symbol.Symbol;
			strategy.Session.Symbol = symbol;

			if (!history.ContainsKey(symbol.Symbol))
			{
				history.Add(symbol.Symbol, new HistoryList());
			}
		}
		#endregion

		#region -- Dialogs --
		private void AddSymbolMenuItem_Click(object sender, EventArgs e)
		{
			var input = Microsoft.VisualBasic.Interaction.InputBox("Add a new ticker symbol", "New Symbol", string.Empty, -1, -1);

			if (string.IsNullOrWhiteSpace(input))
			{
				return;
			}

			symbol = new TradeSymbol() { Symbol = input };
			symbols.Add(symbol);

			//symbols.Sort((a, b) => a.Symbol.CompareTo(b.Symbol));
			SymbolComboBox.SelectedIndex = symbols.IndexOf(symbol);
		}

		private void SaveSymbolMenuItem_Click(object sender, EventArgs e)
		{
			var file = symbol.Symbol.Replace("/", string.Empty);

			File.WriteAllText(SymbolsPath + file + SymbolsExtension, JsonConvert.SerializeObject(symbol));
		}

		private void SaveMenuItem_Click(object sender, EventArgs e)
		{
			SaveSolution();
		}

		private void SaveAsMenuItem_Click(object sender, EventArgs e)
		{
			SaveSolution(true);
		}

		private void SaveSolution(bool force = false)
		{
			if (force || string.IsNullOrWhiteSpace(filename))
			{
				if (!string.IsNullOrWhiteSpace(filename))
				{
					saveDialog.FileName = Path.GetFileName(filename);
				}

				if (saveDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

				filename = saveDialog.FileName;
				openDialog.InitialDirectory = Path.GetDirectoryName(filename);
				saveDialog.InitialDirectory = Path.GetDirectoryName(filename);

				SetTitle(Path.GetFileName(filename));
			}

			solution.Settings = JsonConvert.SerializeObject(strategy.Settings);

			File.WriteAllText(filename, JsonConvert.SerializeObject(solution));
		}

		private void SetTitle(string solution)
		{
			Text = string.Format("Elevated Trader ({0})", solution);
		}

		private void OpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenSolution();
		}

		private void OpenSolution()
		{
			openDialog.FileName = string.Empty;

			if (openDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

			filename = openDialog.FileName;
			openDialog.InitialDirectory = Path.GetDirectoryName(filename);
			saveDialog.InitialDirectory = Path.GetDirectoryName(filename);

			var obj = JsonConvert.DeserializeObject<SolutionSettings>(File.ReadAllText(filename));

			var symbolIndex = symbols.IndexOf((from x in symbols where x.Symbol == obj.Symbol select x).Single());
			var strategyIndex = strategies.IndexOf(obj.Strategy);

			if (strategyIndex < 0)
			{
				MessageBox.Show("The selected strategy is not currently available");
				return;
			}

			SetTitle(Path.GetFileName(filename));

			SymbolComboBox.SelectedIndex = symbolIndex;
			StrategiesComboBox.SelectedIndex = strategyIndex;

			strategy.Settings = JsonConvert.DeserializeObject(obj.Settings, strategy.SettingsType);
			StrategySettings.SelectedObject = strategy.Settings;
			solution = obj;
		}
		#endregion

		#region -- Simulation --
		private void SetState(ApplicationState state)
		{
			StateStatusLabel.Text = state.ToString();
		}

		private async void LoadDataMenuItem_Click(object sender, EventArgs e)
		{
			if (busy) return;
			busy = true;

			await ExecuteLoadTickData();

			busy = false;
		}

		private async Task ExecuteLoadTickData()
		{
			SetState(ApplicationState.Loading);
			await Task.Run(() => LoadTickData());
			SetState(ApplicationState.Idle);
		}

		private void StopLoadingMenuItem_Click(object sender, EventArgs e)
		{
			busy = false;
		}

		public class OldDataFormat
		{
			public double AskPrice { get; set; }
			public double BidPrice { get; set; }
			public double Price { get; set; }
		}

		private void LoadTickData()
		{
			Action<int> update_count = count => { TickCountStatusLabel.Text = count.ToString(); };

			var history_list = history[symbol.Symbol];

			if (settings.TickDataCount < history_list.TickCount) return;

			using (var connection = new SqlConnection(settings.ConnectionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "select top(@count) json, type, id from symbolhistory where symbol = @symbol and type = 3 and id > @id order by id asc";
					command.Parameters.Add(new SqlParameter("@symbol", symbol.Symbol));
					command.Parameters.Add(new SqlParameter("@count", settings.TickDataCount - history_list.TickCount));
					command.Parameters.Add(new SqlParameter("@id", history_list.MaxId));

					using (var reader = command.ExecuteReader())
					{
						int count = 0;

						while (reader.Read())
						{
							var type = (TradeHistoryType)reader.GetInt32(1);

							switch (type)
							{
								case TradeHistoryType.TimeAndSale:
									history_list.MaxId = Math.Max(history_list.MaxId, reader.GetInt64(2));

									var ts = JsonConvert.DeserializeObject<TradeHistoryTimeAndSale>(reader.GetString(0));

									var tick = new TradeTick()
									{
										Ask = ts.AskPrice,
										Bid = ts.BidPrice,
										Price = ts.Price
									};

									if (history_list.Tick != null)
									{
										history_list.Differences.Add(tick - history_list.Tick);
									}

									history_list.Tick = tick;
									history_list.TickCount++;
									break;
							}

							if (++count % 25000 == 0)
							{
								this.Invoke(update_count, count);
							}

							if (!busy) break;
						}

						this.Invoke(update_count, count);
					}
				}

				connection.Close();
			}
		}

		const int ProgressStepValue = 100000;

		private async void RunSimulationMenuItem_Click(object sender, EventArgs e)
		{
			if (busy) return;
			busy = true;

			var history_list = history[symbol.Symbol];

			if (history_list.TickCount == 0 && !settings.GenerateTickData)
			{
				if (MessageBox.Show("Would you like to load tick data?", "Tick Data", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
				{
					busy = false;
					return;
				}
				else
				{
					await ExecuteLoadTickData();

					if (!busy) return;
				}
			}

			TradeChart.Series.Clear();
			periodSeries.Clear();
			tradeSeries.Clear();
			indicatorSeries.Clear();
			tradeBuffer.Clear();

			if (trades != null)
			{
				trades.Clear();
				TradesBindingSource.DataSource = null;
			}

			TradeChart.Visible = false;

			try
			{
				SimulationProgress.Value = 0;
				SimulationProgress.Maximum = settings.GenerateTickData ? settings.TickDataCount : history_list.TickCount;
				SimulationProgress.Step = ProgressStepValue;
				SetState(ApplicationState.Running);

				await Task.Run(() => RunSimulation());

				SetState(ApplicationState.Idle);
				SimulationProgress.Value = 0;

				trades = new BindingList<ITrade>(strategy.Session.Trades.ToList());
				TradesBindingSource.DataSource = trades;

				PopulateTickSeries();

				PopulateIndicatorSeries();

				PopulateTradeSeries();
			}
			finally
			{
				strategy.Clear();

				GC.Collect();
				GC.WaitForPendingFinalizers();

				busy = false;
				TradeChart.Visible = true;
			}
		}

		private void RunSimulation()
		{
			var runner = new TradingStrategyRunner();

			var tick_provider = settings.GenerateTickData
				? (ITradeTickProvider<TickDelta>)new MonteCarloTickProvider()
				: (ITradeTickProvider<TickDelta>)new HistoricalTickProvider()
				;

			runner.Tick += (sender, index) =>
			{
				Action step = () =>
				{
					SimulationProgress.PerformStep();
				};

				if ((index + 1) % ProgressStepValue == 0)
				{
					this.Invoke(step);
				}

				if (index >= settings.TickDataCount || !busy)
				{
					((ITradingStrategyRunner)sender).Stop();
				}
			};

			var history_list = history[symbol.Symbol];

			tick_provider.Initialize(history_list.Tick.Price, history_list.Differences);

			runner.Run(strategy, tick_provider);
		}

		private void StopSimulationMenuItem_Click(object sender, EventArgs e)
		{
			busy = false;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			busy = false;
		}

		private void SetDataCountMenuItem_Click(object sender, EventArgs e)
		{
			var input = Microsoft.VisualBasic.Interaction.InputBox("Set the number of ticks you want to load", "Data Count", settings.TickDataCount.ToString(), -1, -1);

			if (string.IsNullOrWhiteSpace(input)) return;

			try
			{
				settings.TickDataCount = int.Parse(input);
			}
			catch { }
		}
		#endregion

		#region -- Chart Setup --
		private void PopulateTradeSeries()
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

			TickCountStatusLabel.Text = strategy.Session.Trades.Count.ToString();
		}

		private void PopulateIndicatorSeries()
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

		private void PopulateTickSeries()
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

		private DataPoint CreateTradeDataPoint(int size, ITrade trade)
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

		private void ShowGraphMenuItem_Click(object sender, EventArgs e)
		{
			TradeInfoSplitContainer.Panel1Collapsed = !TradeInfoSplitContainer.Panel1Collapsed;
			SetShowGraphCheckedState();
		}

		private void SetShowGraphCheckedState()
		{
			ShowGraphMenuItem.Checked = !TradeInfoSplitContainer.Panel1Collapsed;
			ShowGraphMenuItem.CheckState = ShowGraphMenuItem.Checked ? CheckState.Checked : CheckState.Unchecked;
		}

		private void generateTicksMenuItem_Click(object sender, EventArgs e)
		{
			generateTicksMenuItem.Checked = !generateTicksMenuItem.Checked;
			settings.GenerateTickData = generateTicksMenuItem.Checked;
		}

		private void CalculateStatisticsMenuItem_Click(object sender, EventArgs e)
		{
			//var history_list = history[symbol.Symbol];

			//if (history_list.Differences.Count < 1000) return;

			//var distribution = Normal.Estimate(history_list.Differences);

			//symbol.TickDeviation = distribution.StdDev;
			//symbol.TickMean = distribution.Mean;

			//SymbolProperties.Refresh();
		}

		private void ShuffleLoadedDataMenuItem_Click(object sender, EventArgs e)
		{
			ShuffleLoadedDataMenuItem.Checked = !ShuffleLoadedDataMenuItem.Checked;
			settings.ShuffleGeneratedTickData = ShuffleLoadedDataMenuItem.Checked;
		}
	}
}