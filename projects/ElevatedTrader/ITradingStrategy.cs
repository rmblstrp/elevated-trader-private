using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingStrategy
	{
		ITradeTickAggregator Ticks { get; }

		ITradingSession Session { get; }

		object Settings { get; }

		ITradeSymbol Symbol { get; set; }

		void AddTick(ITradeTick tick);

		void Reset();
	}
}
