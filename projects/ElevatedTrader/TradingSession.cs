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

		public ITradingPeriodAggregator PeriodAggregator
		{
			get;
			set;
		}

		public double Equity
		{
			get;
			protected set;
		}

		public IFinancialInstrument Instrument
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
			AddTrade(TradeType.Buy, quantity);
		}

		public void Sell(int quantity = 1)
		{
			AddTrade(TradeType.Sell, -quantity);
		}

		public void Reverse()
		{
			if (Position == 0)
			{
				throw new ArgumentOutOfRangeException("Only open positions may be reversed");
			}

			var type = Position > 0 ? TradeType.Sell : TradeType.Buy;
			AddTrade(type, Position * -2);
		}

		public void ClosePosition()
		{
			if (Position == 0) return;

			var type = Position > 0 ? TradeType.Sell : TradeType.Buy;
			AddTrade(type, Position);
		}

		public void Reset()
		{
			Position = 0;
			Equity = 0;
			trades.Clear();
		}

		protected void AddTrade(TradeType type, int quantity)
		{
			var price = type == TradeType.Buy ? PeriodAggregator.Last.Ask : PeriodAggregator.Last.Bid;
			var open_cost = Instrument.HasOpenCost ? 1 : 0;

			double trade_cost = 0;

			if (Position == 0)
			{
				Position += quantity;
				trade_cost = Math.Abs(Position * Instrument.PerQuantityCost) - Instrument.PerTradeCost;
				Equity += (price * Position * open_cost) - trade_cost - CalculateSlippage(Position);

				var trade = new TradeEntry(type, quantity, price, Equity, 0, trade_cost, PeriodAggregator.Indexes());
				trades.Add(trade);
				DoOnTrade(trade);
				return;
			}

			var last = trades[trades.Count - 1];
			var price_difference = price - last.Price;
			var tick_change = price_difference / Instrument.TickRate;
			var value = tick_change * Instrument.TickValue;
			var profit = (value * Position) - Math.Abs(Position * Instrument.PerQuantityCost) - CalculateSlippage(Position);

			Position += quantity;
			trade_cost = Math.Abs(Position * Instrument.PerQuantityCost) - Instrument.PerTradeCost;
			Equity += profit + Math.Abs(price * Position * open_cost) - trade_cost - CalculateSlippage(Position);

			var order = new TradeEntry(type, quantity, price, Equity, profit, trade_cost, PeriodAggregator.Indexes());

			trades.Add(order);

			DoOnTrade(order);
		}

		protected double CalculateSlippage(int position)
		{
			return Math.Abs(position * Instrument.TickValue * random.Next(Instrument.Slippage));
		}

		protected void DoOnTrade(ITradeEntry trade)
		{
			if (Trade != null)
			{
				Trade(this, trade);
			}
		}
	}
}
