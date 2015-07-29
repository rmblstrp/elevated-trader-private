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
	public static class Symbol
	{
		public class SymbolContainer
		{
			private Dictionary<string, ITickDataSource> dataSources = new Dictionary<string, ITickDataSource>();

			public TradeSymbol Symbol
			{
				get;
				set;
			}

			public Dictionary<string, ITickDataSource> DataSources
			{
				get { return dataSources; }
			}
		}

		private const string Filename = "symbols.json";
		private static Dictionary<string, SymbolContainer> symbols = new Dictionary<string, SymbolContainer>();

		public static IList<string> Symbols
		{
			get
			{
				return symbols.Keys.ToList();
			}
		}

		static Symbol()
		{
			var filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + Filename;

			if (!File.Exists(filename)) return;

			foreach (var item in JsonConvert.DeserializeObject<List<TradeSymbol>>(File.ReadAllText(filename)))
			{
				Create(item);
			}
		}

		public static void Create(string symbol)
		{
			if (symbols.ContainsKey(symbol)) return;

			Create(new TradeSymbol() { Symbol = symbol });
		}

		private static void Create(TradeSymbol symbol)
		{
			symbols.Add(symbol.Symbol, new SymbolContainer() { Symbol = symbol });
		}
	}
}
