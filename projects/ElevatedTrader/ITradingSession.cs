using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		ITradeSymbol Symbol { get; set; }

		IList<ITrade> Trades { get; }

		void Buy(ITradeTickAggregator ticks, int quantity = 1);
		void Sell(ITradeTickAggregator ticks, int quantity = 1);
		void Reverse(ITradeTickAggregator ticks);

		void Reset();
	}
}
