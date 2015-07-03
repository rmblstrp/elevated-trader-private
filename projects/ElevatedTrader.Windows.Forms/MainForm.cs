namespace ElevatedTrader.Windows.Forms
{
	using System;
	using System.Collections.Generic;
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

			public object StrategySettings
			{
				get;
				set;
			}

			public ITradeSymbol Symbol
			{
				get;
				set;
			}
		}

		private BindingSource symbolsBindingSource = new BindingSource();
		private List<TradeSymbol> symbols = new List<TradeSymbol>();

		private Dictionary<string, Type> strategies = new Dictionary<string, Type>();

		private SolutionSettings settings = new SolutionSettings();
		private FileSystemWatcher indicatorsWatcher;
		private FileSystemWatcher strategiesWatcher;
		private ITradingStrategy strategy;

		const string SymbolsPath = @"symbols\";
		const string IndicatorsPath = @"indicators\";
		const string StrategiesPath = @"strategies\";
		const string ScriptsAssembly = "IndicatorStrategies";
		const string ScriptsFilter = "*.cs";
		const string SymbolsFilter = "*.json";


		Assembly scripts_assembly = null;
		private string[] reference_assemblies = new string[]
		{
			"ElevatedTrader",
			"MathNet.Numerics"
		};

		public MainForm()
		{
			InitializeComponent();



			settings.Symbol = new TradeSymbol();
			settings.Strategy = "HullStrategy.cs";

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

			LoadSymbols();
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
				strategies.Add(item.FullName, item);
				StrategiesComboBox.Items.Add(item.Name);
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
			if (strategy == null)
			{
				// should probably do other cleanup here;
				strategy = null;
			}

			strategy = (ITradingStrategy)scripts_assembly.CreateInstance(name);
			StrategySettings.SelectedObject = strategy.Settings;
		}

		private void LoadSymbols()
		{
			var files = Directory.EnumerateFiles(SymbolsPath, SymbolsFilter, SearchOption.TopDirectoryOnly);

			foreach (var file in files)
			{
				var item = JsonConvert.DeserializeObject<TradeSymbol>(File.ReadAllText(file));

				symbols.Add(item);
			}

			symbols.Sort((a, b) => a.Symbol.CompareTo(b.Symbol));

			SymbolComboBox.DataSource = symbols;
		}

		private void StrategiesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitializeStrategy(StrategiesComboBox.Text);
		}

		private void SymbolComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SymbolProperties.SelectedObject = symbols[SymbolComboBox.SelectedIndex];
		}
	}
}
