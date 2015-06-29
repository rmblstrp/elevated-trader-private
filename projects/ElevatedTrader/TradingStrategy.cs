using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public abstract class TradingStrategy : ITradingStrategy
	{
		protected ITradingSession session;
		protected ITradingPeriodAggregator ticks;
		protected ITradeSymbol symbol;

		public ITradingPeriodAggregator Ticks
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
			get { return symbol; }
			set { session.Symbol = symbol = value; }
		}

		public TradingStrategy()
		{
			session = new TradingSession();
			ticks = new TradingPeriodAggregator();

			ticks.BeforeNewPeriod += BeforeNewPeriod;
		}

		public virtual void AddTick(ITradeTick tick)
		{
			ticks.AddTick(tick);
		}

		protected virtual void BeforeNewPeriod(int size)
		{
		}

		public virtual void Reset()
		{
			session.Reset();
			ticks.Reset();
		}
	}
}
