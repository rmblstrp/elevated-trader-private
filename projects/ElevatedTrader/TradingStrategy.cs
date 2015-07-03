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
		protected ITradingPeriodAggregator aggregator;
		protected ITradeSymbol symbol;

		public ITradingPeriodAggregator Periods
		{
			get { return aggregator; }
		}

		public virtual ITradingSession Session
		{
			get { return session; }
		}

		public abstract object Settings
		{
			get;
		}

		public abstract Type SettingsType
		{
			get;
		}

		public TradingStrategy()
		{
			session = new TradingSession();
			aggregator = new TradingPeriodAggregator();

			aggregator.BeforeNewPeriod += BeforeNewPeriod;
		}

		public virtual void AddTick(ITradeTick tick)
		{
			aggregator.AddTick(tick);
		}

		protected virtual void BeforeNewPeriod(int size)
		{
		}

		public virtual void Initialize()
		{
			session.Reset();
			aggregator.Reset();
		}
	}
}
