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
		protected List<ITradeEntry> trades = new List<ITradeEntry>();
		protected Random random = new Random();
		protected ITradeExecutor executor;

		public double Equity
		{
			get;
			protected set;
		}

		public ITradeExecutor Executor
		{
			get { return executor; }
			set
			{
				if (executor != null)
				{
					UnlinkExecutorEvents(executor);
				}

				executor = value;
				LinkExecutorEvents(executor);
			}
		}

		public IFinancialInstrument Instrument
		{
			get;
			set;
		}

		public ITradingPeriodAggregator PeriodAggregator
		{
			get;
			set;
		}

		public int Position
		{
			get;
			protected set;
		}

		public IList<ITradeEntry> Trades
		{
			get { return trades; }
		}

		public event EventHandler<ITradeEntry> Trade;

		public void Buy(int quantity = 1)
		{
			ExecuteTrade(quantity);
		}

		public void Sell(int quantity = 1)
		{
			ExecuteTrade(-quantity);
		}

		public void Reverse()
		{
			if (Position == 0)
			{
				throw new ArgumentOutOfRangeException("Only open positions may be reversed");
			}

			ExecuteTrade(Position * -2);
		}

		public void ClosePosition()
		{
			if (Position == 0) return;

			ExecuteTrade(-Position);
		}

		public void Reset()
		{
			Position = 0;
			Equity = 0;
			trades.Clear();
		}

		protected void ExecuteTrade(int quantity)
		{
			var order = new TradeOrder()
			{
				Instrument = Instrument,
				Quantity = quantity,
				Bid = PeriodAggregator.Last.Bid,
				Ask = PeriodAggregator.Last.Ask
			};

			executor.ExecuteTrade(order);			
		}

		protected void DoOnTrade(ITradeEntry trade)
		{
			if (Trade != null)
			{
				Trade(this, trade);
			}
		}

		protected void LinkExecutorEvents(ITradeExecutor executor)
		{
			executor.TradeExecuted += TradeExecuted;
			executor.TradeCancelled += TradeCancelled;
			executor.TradeFailed += TradeFailed;
		}

		protected void UnlinkExecutorEvents(ITradeExecutor executor)
		{
			executor.TradeExecuted -= TradeExecuted;
			executor.TradeCancelled -= TradeCancelled;
			executor.TradeFailed -= TradeFailed;
		}

		protected void TradeExecuted(object sender, ITradeOrder order)
		{
			var price = order.Results[0].Price;
			var open_cost = Instrument.HasOpenCost ? 1 : 0;

			double price_difference = 0;

			if (Position != 0)
			{
				price_difference = price - trades[trades.Count - 1].Price;
			}

			// TODO: improve this so it accounts for adding to positions instead of just opening and reversing
			var tick_change = price_difference / Instrument.TickIncrement;
			var value = tick_change * Instrument.TickValue;
			var profit = (value * Position) - order.Results[0].Fees;

			Position += order.Quantity;
			Equity += profit - Math.Abs(price * Position * open_cost);

			var entry = new TradeEntry(order.Quantity > 0 ? TradeType.Buy : TradeType.Sell, order.Quantity, price, Equity, profit, order.Results[0].Fees, PeriodAggregator.Indexes());

			trades.Add(entry);

			DoOnTrade(entry);
		}

		protected void TradeCancelled(object sender, ITradeOrder order)
		{

		}

		protected void TradeFailed(object sender, ITradeOrder order)
		{

		}
	}
}
