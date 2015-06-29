using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingSession : ITradingSession
	{		
		protected List<ITrade> trades = new List<ITrade>();

		public double Equity
		{
			get;
			protected set;
		}

		public ITradeSymbol Symbol
		{
			get;
			set;
		}

		public int Position
		{
			get;
			protected set;
		}

		public IList<ITrade> Trades
		{
			get { return trades; }
		}

		public void Buy(ITradeTickAggregator ticks, int quantity = 1)
		{
			AddTrade(TradeType.Buy, quantity, ticks);
		}

		public void Sell(ITradeTickAggregator ticks, int quantity = 1)
		{
			AddTrade(TradeType.Sell, -quantity, ticks);
		}

		public void Reverse(ITradeTickAggregator ticks)
		{
			if (Position == 0)
			{
				throw new ArgumentOutOfRangeException("Only open positions may be reversed");
			}

			var type = Position > 0 ? TradeType.Sell : TradeType.Buy;
			AddTrade(type, Position * -2, ticks);
		}

		public void Reset()
		{
			Position = 0;
			trades.Clear();
		}

		protected void AddTrade(TradeType type, int quantity, ITradeTickAggregator ticks)
		{
			var price =  type == TradeType.Buy ? ticks.Last.Ask : ticks.Last.Bid;
			var open_cost = Symbol.HasOpenCost ? 1 : 0;

			if (Position == 0)
			{
				Position += quantity;
				Equity += (price * Position * open_cost) - Math.Abs(Position * Symbol.PerQuanityCost) - Symbol.PerTradeCost;

				trades.Add(new Trade(type, quantity, price, Equity, 0, ticks.Indexes()));
			}

			var last = trades[trades.Count - 1];			
			var price_difference = price - last.Price;
			var tick_change = price_difference / Symbol.TickRate;
			var value = tick_change * Symbol.TickValue;			
			var profit = (price * Position) - Math.Abs(Position * Symbol.PerQuanityCost) - Symbol.PerTradeCost;
			
			Position += quantity;

			Equity += profit - (price * Position * open_cost) - Math.Abs(Position * Symbol.PerQuanityCost) - Symbol.PerTradeCost;

			trades.Add(new Trade(type, quantity, price, Equity, profit, ticks.Indexes()));
		}
	}
}
