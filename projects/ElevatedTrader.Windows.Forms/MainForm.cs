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

	public partial class MainForm : Form
	{
		#region -- Internal Classes --
		public class ApplicationSettings
		{
			public string DataConnetionString
			{
				get;
				set;
			}
		}

		public class SolutionSettings
		{
			public string Strategy
			{
				get;
				set;
			}

			public string Settings
			{
				get;
				set;
			}

			public string Symbol
			{
				get;
				set;
			}
		}

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
		private ApplicationSettings application = new ApplicationSettings()
		{
			DataConnetionString = @"Data Source=localhost\sqlexpress;Initial Catalog=AutomatedTrading;Integrated Security=True"
			//DataConnetionString = @"Data Source=thecodewerks.com;Initial Catalog=AutomatedTrading;Persist Security Info=True;User ID=elevated-trader;Password=Phuducran+7rafre"
		};

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
			"MathNet.Numerics"
		};

		private List<TradeTick> ticks = new List<TradeTick>(10000000);
		private BindingList<ITrade> trades;
		private List<ITrade> tradeBuffer = new List<ITrade>(10000);
		private Dictionary<int, Series> periodSeries = new Dictionary<int, Series>();
		private Dictionary<int, Series> tradeSeries = new Dictionary<int, Series>();
		private Dictionary<IIndicator, Series> indicatorSeries = new Dictionary<IIndicator, Series>();
		private int dataCount = int.MaxValue;
		#endregion

		#region -- Constants --

		const string PathBase = @"library\";
		const string SymbolsPath = @"symbols\";
		const string IndicatorsPath = PathBase + @"indicators\";
		const string StrategiesPath = PathBase + @"strategies\";

		const string ScriptsFilter = "*.cs";

		const string SymbolsExtension = ".json";
		const string SymbolsFilter = "*" + SymbolsExtension;

		const string SolutionExtension = ".json";
		const string SolutionFilter = "JSON Solution|*" + SolutionExtension;
		#endregion

		public MainForm()
		{
			InitializeComponent();

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
				InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			};

			saveDialog = new SaveFileDialog()
			{
				DefaultExt = SolutionExtension,
				FileName = string.Empty,
				Filter = SolutionFilter,
				InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			};

			StrategiesComboBox.DataSource = strategies;
			SetShowGraphCheckedState();
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
		}
		#endregion

		#region -- Dialogs --
		private void AddSymbolMenuItem_Click(object sender, EventArgs e)
		{
			var input = Microsoft.VisualBasic.Interaction.InputBox("Add a new ticker symbol", "New Symbol", string.Empty, -1, -1);

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
				if (saveDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

				filename = saveDialog.FileName;
				openDialog.InitialDirectory = Path.GetDirectoryName(filename);
			}

			solution.Settings = JsonConvert.SerializeObject(strategy.Settings);

			File.WriteAllText(filename, JsonConvert.SerializeObject(solution));
		}

		private void OpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenSolution();
		}

		private void OpenSolution()
		{
			openDialog.FileName = string.Empty;

			if (openDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

			var obj = JsonConvert.DeserializeObject<SolutionSettings>(File.ReadAllText(openDialog.FileName));

			var symbolIndex = symbols.IndexOf((from x in symbols where x.Symbol == obj.Symbol select x).Single());
			var strategyIndex = strategies.IndexOf(obj.Strategy);

			if (strategyIndex < 0)
			{
				MessageBox.Show("The selected strategy is not currently available");
				return;
			}

			SymbolComboBox.SelectedIndex = symbolIndex;
			StrategiesComboBox.SelectedIndex = strategyIndex;

			strategy.Settings = (TradingStrategySettings)JsonConvert.DeserializeObject(obj.Settings, strategy.SettingsType);
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

			//{"AskPrice":1.111,"BidPrice":1.1109,"EventId":6157792271241579016,"ExchangeCode":"\u0000","IsTrade":true,"Price":1.1109,"Size":1,"Time":"2015-06-08T00:18:58Z","Type":0}
			using (var connection = new SqlConnection(application.DataConnetionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					//command.CommandText = "select top(@count) json from quotedata where symbol = @symbol";
					command.CommandText = "select top(@count) json, type from symbolhistory where symbol = @symbol";
					command.Parameters.Add(new SqlParameter("@symbol", symbol.Symbol));
					command.Parameters.Add(new SqlParameter("@count", dataCount));

					using (var reader = command.ExecuteReader())
					{
						ticks.Clear();

						int count = 0;

						while (reader.Read())
						{
							//var item = JsonConvert.DeserializeObject<OldDataFormat>(reader.GetString(0));							

							var type = (TradeHistoryType)reader.GetInt32(1);

							switch (type)
							{
								case TradeHistoryType.TimeAndSale:
									var item = JsonConvert.DeserializeObject<TradeHistoryTimeAndSale>(reader.GetString(0));
									ticks.Add
									(
										new TradeTick()
										{
											Ask = item.AskPrice,
											Bid = item.BidPrice,
											Price = item.Price
										}
									);
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

				busy = false;
				connection.Close();
			}
		}

		private async void RunSimulationMenuItem_Click(object sender, EventArgs e)
		{
			if (busy) return;
			busy = true;

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

			LinkSession();
			LinkAggregrator();

			try
			{
				SetState(ApplicationState.Running);
				await Task.Run(() => RunSimulation());
				SetState(ApplicationState.Idle);

				trades = new BindingList<ITrade>(tradeBuffer);
				TradesBindingSource.DataSource = trades;
			}
			finally
			{
				UnlinkSession();
				UnlinkAggregator();
			}
		}

		private void RunSimulation()
		{
			const int StepValue = 250;

			Action<int, int> set_maximum = (max, inc) => { SimulationProgress.Maximum = max; SimulationProgress.Step = inc; };
			Action set_value = () => { SimulationProgress.Value = 0; };
			Action increment = () => { SimulationProgress.Increment(1); };
			Action step = () => { SimulationProgress.PerformStep(); };

			this.Invoke(set_maximum, ticks.Count, StepValue);

			strategy.Initialize();

			for (int index = 0; index < ticks.Count; index++)
			{
				var item = ticks[index];

				strategy.AddTick(item);

				if ((index + 1) % StepValue == 0)
				{
					this.Invoke(step);
				}

				if (!busy) break;
			}

			busy = false;

			this.Invoke(set_value);
		}

		private void StopSimulationMenuItem_Click(object sender, EventArgs e)
		{
			busy = false;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			busy = false;
		}
		#endregion

		#region -- Session Data --
		private bool session_ontrade = false;
		private void LinkSession()
		{
			if (strategy.Session.Trades is BindingList<ITrade>)
			{
				TradesBindingSource.DataSource = strategy.Session.Trades as BindingList<ITrade>;
			}
			else
			{
				strategy.Session.Trade += OnTrade;
				session_ontrade = true;
			}
		}

		private void UnlinkSession()
		{
			if (session_ontrade)
			{
				strategy.Session.Trade -= OnTrade;
				session_ontrade = false;
			}
		}

		private void OnTrade(object sender, ITrade trade)
		{
			Action<ITrade> a = (order) =>
			{
				tradeBuffer.Add(order);

				foreach (var kv in periodSeries)
				{
					if (!tradeSeries.ContainsKey(kv.Key))
					{
						var ts = CreateTradeSeries();

						tradeSeries.Add(kv.Key, ts);
						TradeChart.Series.Add(ts);
					}

					tradeSeries[kv.Key].Points.Add(CreateTradeDataPoint(kv.Key, order));
				}

				TickCountStatusLabel.Text = tradeBuffer.Count.ToString();
			};

			this.Invoke(a, trade);
		}

		private void SetDataCountMenuItem_Click(object sender, EventArgs e)
		{
			var input = Microsoft.VisualBasic.Interaction.InputBox("Set the number of ticks you want to load", "Data Count", dataCount.ToString(), -1, -1);

			dataCount = int.Parse(input);
		}

		private void LinkAggregrator()
		{
			periodSeries.Clear();
			strategy.Aggregator.BeforeNewPeriod += Aggregator_BeforeNewPeriod;
		}

		private void UnlinkAggregator()
		{
			strategy.Aggregator.BeforeNewPeriod -= Aggregator_BeforeNewPeriod;
		}

		private void Aggregator_BeforeNewPeriod(int size)
		{
			if (!periodSeries.ContainsKey(size))
			{
				var ps = CreatePeriodSeries();
				periodSeries.Add(size, ps);

				Action<Series> psa = psx =>
				{
					TradeChart.Series.Add(psx);
				};

				this.Invoke(psa, ps);
			}

			var ind = strategy.Indicators[size][0];

			if (!indicatorSeries.ContainsKey(ind))
			{
				var series = CreateIndicatorSeries();

				Action<IIndicator, Series> inda = (inds, indx) =>
				{
					indicatorSeries.Add(inds, indx);
					TradeChart.Series.Add(indx);
				};

				this.Invoke(inda, ind, series);
			}

			var item = strategy.Aggregator.Periods[size];

			Action<int, DataPoint, IIndicator, DataPoint> pda = (idx, pdx, indx, indp) =>
			{
				periodSeries[idx].Points.Add(pdx);

				if (indp != null)
				{
					indicatorSeries[indx].Points.Add(indp);
				}
			};

			this.Invoke(pda, size, CreatePeriodDataPoint(item), ind, CreateIndicatorDataPoint(ind));
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
				YAxisType = AxisType.Secondary
			};

			return item;
		}

		private DataPoint CreatePeriodDataPoint(IList<ITradingPeriod> periods)
		{
			var period = periods[periods.Count - 1];

			var item = new DataPoint()
			{
				XValue = periods.Count,
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
		private DataPoint CreateIndicatorDataPoint(IIndicator indicator)
		{
			var last = indicator.Results[indicator.Results.Count - 1];

			if (last.Values.Count == 0)
			{
				return null;
			}

			var item = new DataPoint()
			{
				XValue = indicator.Results.Count,
				YValues = new double[] { last.Values[0] },
				Color = last.Direction == TrendDirection.Rising ? Color.LightGreen : Color.LightPink
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
	}
}