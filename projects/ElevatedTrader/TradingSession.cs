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
			AddTrade(TradeType.Reverse, Position * -2, ticks);
		}

		public void Reset()
		{
			Position = 0;
			trades.Clear();
		}

		protected void AddTrade(TradeType type, int quantity, ITradeTickAggregator ticks)
		{
			var price = ticks.Last.Last;

			if (Position == 0 || trades.Count == 0)
			{
				if (type == TradeType.Reverse)
				{
					throw new ArgumentOutOfRangeException("Only open positions may be reversed");
				}

				Position += quantity;
				Equity += (price * Position * Symbol.OpenCost) - Math.Abs(Position * Symbol.PerQuanityCost) - Symbol.PerTradeCost;

				trades.Add(new Trade(type, quantity, price, Equity, 0, ticks.Indexes()));
			}

			var last = trades[trades.Count - 1];			
			var price_difference = price - last.Price;
			var tick_change = price_difference / Symbol.TickRate;
			var value = tick_change * Symbol.TickValue;
			var actual_type = quantity > 0 ? TradeType.Buy : TradeType.Sell;
			var profit = (price * Position) - Math.Abs(Position * Symbol.PerQuanityCost) - Symbol.PerTradeCost;
			
			Position += quantity;

			Equity += profit - (price * Position * Symbol.OpenCost) - Math.Abs(Position * Symbol.PerQuanityCost) - Symbol.PerTradeCost;
		}
	}
}
