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

		IDictionary<int, IList<IIndicator>> Indicators { get; }

		ITradingSession Session { get; }

		TradingStrategySettings Settings { get; set;  }

		Type SettingsType { get; }

		void AddTick(ITradeTick tick);

		void Initialize();
	}
}
