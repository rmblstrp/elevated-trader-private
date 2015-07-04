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

		ITradingSession Session { get; }

		object Settings { get; set;  }

		Type SettingsType { get; }

		void AddTick(ITradeTick tick);

		void Initialize();
	}
}
