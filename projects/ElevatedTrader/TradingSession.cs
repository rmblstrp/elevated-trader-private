using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		public event EventHandler<ITrade> Trade;

		public void Buy(ITradingPeriodAggregator aggregator, int quantity = 1)
		{
			AddTrade(TradeType.Buy, quantity, aggregator);
		}

		public void Sell(ITradingPeriodAggregator aggregator, int quantity = 1)
		{
			AddTrade(TradeType.Sell, -quantity, aggregator);
		}

		public void Reverse(ITradingPeriodAggregator aggregator)
		{
			if (Position == 0)
			{
				throw new ArgumentOutOfRangeException("Only open positions may be reversed");
			}

			var type = Position > 0 ? TradeType.Sell : TradeType.Buy;
			AddTrade(type, Position * -2, aggregator);
		}

		public void Reset()
		{
			Position = 0;
			Equity = 0;
			trades.Clear();
		}

		protected void AddTrade(TradeType type, int quantity, ITradingPeriodAggregator ticks)
		{
			//var price =  type == TradeType.Buy ? ticks.Last.Ask : ticks.Last.Bid;
			var price = ticks.Last.Price;
			var open_cost = Symbol.HasOpenCost ? 1 : 0;

			if (Position == 0)
			{
				Position += quantity;
				Equity += (price * Position * open_cost) - Math.Abs(Position * Symbol.PerQuantityCost) - Symbol.PerTradeCost;

				var trade = new Trade(type, quantity, price, Equity, 0, ticks.Indexes());
				trades.Add(trade);
				DoOnTrade(trade);
				return;
			}

			var last = trades[trades.Count - 1];			
			var price_difference = price - last.Price;
			var tick_change = price_difference / Symbol.TickRate;
			var value = tick_change * Symbol.TickValue;			
			var profit = (value * Position) - Math.Abs(Position * Symbol.PerQuantityCost);

			// position + quantity = a
			// position - quantity = b
			// a - b - position = ???
			
			Position += quantity;

			Equity += profit + Math.Abs(price * Position * open_cost) - Math.Abs(Position * Symbol.PerQuantityCost) - Symbol.PerTradeCost;

			var order = new Trade(type, quantity, price, Equity, profit, ticks.Indexes());

			trades.Add(order);

			DoOnTrade(order);
		}

		protected void DoOnTrade(ITrade trade)
		{
			if (Trade != null)
			{
				Trade(this, trade);
			}
		}
	}
}
