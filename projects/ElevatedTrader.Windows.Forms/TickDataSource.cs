using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElevatedTrader.Windows.Forms
{
	public static class TickDataSource
	{
		private class DataSourceConfiguration
		{
			public Type Type
			{
				get;
				set;
			}

			public JObject Settings
			{
				get;
				set;
			}
		}

		private const string Filename = "datasources.json";
		private static Dictionary<string, DataSourceConfiguration> configurations = new Dictionary<string, DataSourceConfiguration>();

		public static IList<string> Sources
		{
			get
			{
				return configurations.Keys.ToList();
			}
		}

		static TickDataSource()
		{
			var filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + Filename;

			if (!File.Exists(filename)) return;

			foreach (dynamic item in JToken.Parse(File.ReadAllText(filename)))
			{
				var type = Type.GetType((string)item.Type);

				var source = new DataSourceConfiguration()
				{
					Type = type,
					Settings = item.Settings
				};

				configurations.Add((string)item.Name, source);
			}
		}

		public static ITickDataSource Create(string source)
		{
			if (!configurations.ContainsKey(source))
			{
				throw new KeyNotFoundException(source);
			}

			var config = configurations[source];

			var instance = (ITickDataSource)Activator.CreateInstance(config.Type);

			instance.Configure(config.Settings.ToObject<ExpandoObject>());

			return instance;
		}
	}
}
