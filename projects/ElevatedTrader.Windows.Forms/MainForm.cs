namespace ElevatedTrader.Windows.Forms
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using CSScriptLibrary;
	using Newtonsoft.Json;

	public partial class MainForm : Form
	{
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

		private string filename = null;

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

		const string SymbolsPath = @"symbols\";
		const string IndicatorsPath = @"indicators\";
		const string StrategiesPath = @"strategies\";

		const string ScriptsFilter = "*.cs";

		const string SymbolsExtension = ".json";
		const string SymbolsFilter = "*" + SymbolsExtension;

		const string SolutionExtension = ".json";
		const string SolutionFilter = "JSON Solution|*" + SolutionExtension;


		Assembly scripts_assembly = null;
		private string[] reference_assemblies = new string[]
		{
			"ElevatedTrader",
			"MathNet.Numerics"
		};

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

		void indicatorsWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			LoadScripts();
		}

		void strategiesWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			LoadScripts();
		}

		private void LoadScripts()
		{
			var files = ListStrategyFiles().Union(ListIndicatorsFiles()).ToArray();

			scripts_assembly = Assembly.LoadFile(CSScript.CompileFiles(files, reference_assemblies));

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
			if (strategy == null)
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

		private void StrategiesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitializeStrategy(StrategiesComboBox.Text);
		}

		private void SymbolComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			symbol = symbols[SymbolComboBox.SelectedIndex];
			SymbolProperties.SelectedObject = symbol;

			solution.Symbol = symbol.Symbol;
			strategy.Session.Symbol = symbol;
		}

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

			strategy.Settings = JsonConvert.DeserializeObject(obj.Settings, strategy.SettingsType);
			StrategySettings.SelectedObject = strategy.Settings;
			solution = obj;
		}
	}
}
