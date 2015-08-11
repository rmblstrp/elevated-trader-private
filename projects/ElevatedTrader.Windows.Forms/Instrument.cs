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
	public static class Instrument
	{
		public class InstrumentContainer
		{
			private readonly Dictionary<string, ITickDataSource> dataSources = new Dictionary<string, ITickDataSource>();

			public TradeInstrument Item
			{
				get;
				set;
			}

			public Dictionary<string, ITickDataSource> DataSources
			{
				get { return dataSources; }
			}

			public bool HasDataSource(string name)
			{
				return dataSources.ContainsKey(name);
			}
		}

		private const string Filename = "instruments.json";
		private static readonly Dictionary<string, InstrumentContainer> symbols = new Dictionary<string, InstrumentContainer>();

		public static IList<string> Symbols
		{
			get
			{
				return symbols.Keys.ToList();
			}
		}

		static Instrument()
		{
			var filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + Filename;

			if (!File.Exists(filename)) return;

			foreach (var item in JsonConvert.DeserializeObject<List<TradeInstrument>>(File.ReadAllText(filename)))
			{
				Add(item);
			}
		}

		public static void Add(string symbol)
		{
			if (symbols.ContainsKey(symbol)) return;

			Add(new TradeInstrument() { Symbol = symbol });
		}

		private static void Add(TradeInstrument symbol)
		{
			symbols.Add(symbol.Symbol, new InstrumentContainer() { Item = symbol });
		}

		public static InstrumentContainer Get(string symbol)
		{
			return symbols[symbol];
		}
	}
}
