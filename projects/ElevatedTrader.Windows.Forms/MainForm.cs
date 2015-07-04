namespace ElevatedTrader.Windows.Forms
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Data;
	using System.Data.SqlClient;
	using System.Drawing;
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
			"MathNet.Numerics"
		};

		private List<TradeTick> ticks = new List<TradeTick>(10000000);
		private BindingList<ITrade> trades = new BindingList<ITrade>();
		private Dictionary<int, Series> periodSeries = new Dictionary<int, Series>();
		private Dictionary<int, Series> tradeSeries = new Dictionary<int, Series>();
		private int dataCount = 50000;
		#endregion

		#region -- Constants --

		const string SymbolsPath = @"symbols\";
		const string IndicatorsPath = @"indicators\";
		const string StrategiesPath = @"strategies\";

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


		}

		#region -- Strategies --
		void indicatorsWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			LoadScripts();
			Action a = () =>
			{
				StrategiesComboBox.SelectedIndex = 0;
			};

			this.Invoke(a);
		}

		void strategiesWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			LoadScripts();
			Action a = () =>
			{
				StrategiesComboBox.SelectedIndex = 0;
			};

			this.Invoke(a);
		}

		private void LoadScripts()
		{
			var files = ListStrategyFiles().Union(ListIndicatorsFiles()).ToArray();

			scripts_assembly = Assembly.LoadFile(CSScript.CompileFiles(files, null, true, reference_assemblies));

			var implements = typeof(ITradingStrategy);
			var types = scripts_assembly.GetTypes().Where(t => implements.IsAssignableFrom(t));

			strategies.Clear();

			foreach (var item in types)
			{
				strategies.Add(item.FullName);
			}

			StrategiesComboBox.DataSource = strategies;
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
					command.CommandText = "select top(@count) json from quotedata where symbol = @symbol";
					command.Parameters.Add(new SqlParameter("@symbol", symbol.Symbol));
					command.Parameters.Add(new SqlParameter("@count", dataCount));

					using (var reader = command.ExecuteReader())
					{
						ticks.Clear();

						int count = 0;

						while (reader.Read())
						{
							var item = JsonConvert.DeserializeObject<OldDataFormat>(reader.GetString(0));

							ticks.Add
							(
								new TradeTick()
								{
									Ask = item.AskPrice,
									Bid = item.BidPrice,
									Price = item.Price
								}
							);

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

			LinkSession();
			LinkAggregrator();

			try
			{
				SetState(ApplicationState.Running);
				await Task.Run(() => RunSimulation());
				SetState(ApplicationState.Idle);
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
				trades.Clear();
				TradesBindingSource.DataSource = trades;
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
				trades.Add(order);

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

				TickCountStatusLabel.Text = trades.Count.ToString();
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

			var item = strategy.Aggregator.Periods[size];

			var point = CreatePeriodDataPoint(item[item.Count - 1]);

			Action<int, DataPoint> pda = (idx, pdx) =>
			{
				periodSeries[idx].Points.Add(pdx);
			};

			this.Invoke(pda, size, point);
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

		private DataPoint CreatePeriodDataPoint(ITradingPeriod period)
		{
			var item = new DataPoint()
			{
				YValues = new double[] { period.High, period.Low, period.Open, period.Close }
			};

			return item;
		}

		private DataPoint CreateTradeDataPoint(int size, ITrade trade)
		{
			var item = new DataPoint()
			{
				XValue = trade.Indexes[size] + 1,
				YValues = new double[] { trade.Price }
			};

			return item;
		}
		#endregion		
	}
}
