using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingStrategy
	{
		ITradingPeriodAggregator Aggregator { get; }

		IDictionary<int, IList<ISymbolIndicator>> Indicators { get; }

		ITradingSession Session { get; }

		object Settings { get; set;  }

		Type SettingsType { get; }

		void AddQuote(ITradeQuote quote);

		void AddTick(ITick tick);

		void Clear();

		void FreeResources();		

		void Initialize();

		void End();
	}
}
