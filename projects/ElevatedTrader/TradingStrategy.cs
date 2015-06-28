using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public abstract class TradingStrategy : ITradingStrategy
	{
		protected ITradingSession session = new TradingSession();
		protected ITradeTickAggregator ticks = new TradeTickAggregator();
		protected ITradeSymbol symbol;

		public ITradeTickAggregator Ticks
		{
			get { return ticks; }
		}

		public virtual ITradingSession Session
		{
			get { return session; }
		}

		public abstract object Settings
		{
			get;
		}

		public virtual ITradeSymbol Symbol
		{
			get
			{
				return symbol;
			}
			set
			{
				session.Symbol = symbol = value;
			}
		}

		public virtual void AddTick(ITradeTick tick)
		{
			ticks.Add(tick);
		}

		public virtual void Reset()
		{
			session.Reset();
			ticks.Reset();
		}
	}
}
