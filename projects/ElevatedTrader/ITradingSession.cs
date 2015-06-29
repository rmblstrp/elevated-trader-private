using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		double Equity { get; }

		ITradeSymbol Symbol { get; set; }

		int Position { get; }

		IList<ITrade> Trades { get; }

		void Buy(ITradingPeriodAggregator ticks, int quantity = 1);
		void Sell(ITradingPeriodAggregator ticks, int quantity = 1);
		void Reverse(ITradingPeriodAggregator ticks);

		void Reset();
	}
}
