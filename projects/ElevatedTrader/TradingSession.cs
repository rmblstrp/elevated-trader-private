using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingSession : ITradingSession
	{

		public ITradeSymbol Symbol
		{
			get;
			set;
		}

		public IList<ITrade> Trades
		{
			get;
			protected set;
		}

		public void Buy(ITradeTickAggregator ticks, int quantity = 1)
		{
			throw new NotImplementedException();
		}

		public void Sell(ITradeTickAggregator ticks, int quantity = 1)
		{
			throw new NotImplementedException();
		}

		public void Reverse(ITradeTickAggregator ticks)
		{
			throw new NotImplementedException();
		}
	}
}
