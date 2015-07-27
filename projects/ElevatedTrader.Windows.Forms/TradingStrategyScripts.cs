using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSScriptLibrary;

namespace ElevatedTrader.Windows.Forms
{
	public static class TradingStrategyScripts
	{
		const string ScriptsFilter = "*.cs";
		const string PathBase = @"library\";
		const string IndicatorsPath = @"indicators\";
		const string StrategiesPath = @"strategies\";

		private static Dictionary<string, Type> strategies = new Dictionary<string, Type>();
		private static FileSystemWatcher indicatorsWatcher;
		private static FileSystemWatcher strategiesWatcher;
		private static Assembly scripts_assembly = null;
		private static string[] reference_assemblies = new string[]
		{
			"ElevatedTrader",
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

		public static event Action ScriptsLoaded;

		public static IList<string> Strategies
		{
			get
			{
				return strategies.Keys.ToList();
			}
		}

		static TradingStrategyScripts()
		{
			indicatorsWatcher = new FileSystemWatcher(IndicatorsPath, ScriptsFilter)
			{
				NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
			};
			strategiesWatcher = new FileSystemWatcher(StrategiesPath, ScriptsFilter)
			{
				NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
			};

			indicatorsWatcher.Changed += FileChangedEvent;
			strategiesWatcher.Changed += FileChangedEvent;
		}

		public static ITradingStrategy Create(string name)
		{
			return (ITradingStrategy)scripts_assembly.CreateInstance(name);
		}

		public static void Initialize()
		{
			Load();

			indicatorsWatcher.EnableRaisingEvents = true;
			strategiesWatcher.EnableRaisingEvents = true;
		}

		public static void Load()
		{
			var indicator_files = Directory.EnumerateFiles(PathBase + IndicatorsPath, ScriptsFilter, SearchOption.TopDirectoryOnly);
			var strategy_files = Directory.EnumerateFiles(PathBase + StrategiesPath, ScriptsFilter, SearchOption.TopDirectoryOnly);
			var files = indicator_files.Union(strategy_files).ToArray();

			scripts_assembly = Assembly.LoadFile(CSScript.CompileFiles(files, null, true, reference_assemblies));

			var implements = typeof(ITradingStrategy);
			var types = scripts_assembly.GetTypes().Where(t => implements.IsAssignableFrom(t));

			foreach (var item in types)
			{
				strategies.Add(item.FullName, item);
			}

			if (ScriptsLoaded != null)
			{
				ScriptsLoaded();
			}
		}

		private static void FileChangedEvent(object sender, FileSystemEventArgs e)
		{
			Load();
		}
	}
}
