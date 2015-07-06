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
		protected Dictionary<int, IList<IIndicator>> indicators = new Dictionary<int,IList<IIndicator>>();

		public ITradingPeriodAggregator Aggregator
		{
			get { return aggregator; }
		}

		public IDictionary<int, IList<IIndicator>> Indicators
		{
			get { return indicators; }
		}

		public virtual ITradingSession Session
		{
			get { return session; }
		}

		public abstract object Settings
		{
			get;
			set;
		}

		public abstract Type SettingsType
		{
			get;
		}

		public TradingStrategy()
		{
			session = new TradingSession();
			aggregator = new TradingPeriodAggregator();

			aggregator.AfterNewPeriod += AfterNewPeriod;
			aggregator.BeforeNewPeriod += BeforeNewPeriod;
		}

		public virtual void AddTick(ITradeTick tick)
		{
			aggregator.AddTick(tick);
		}

		protected virtual void AfterNewPeriod(int size)
		{
		}

		protected virtual void BeforeNewPeriod(int size)
		{
		}

		public virtual void Initialize()
		{
			session.Reset();
			aggregator.Reset();
			indicators.Clear();
		}
	}
}
