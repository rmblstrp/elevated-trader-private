using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public abstract class TradingStrategy : TradingStrategy<TradingStrategySettings>
	{
	}

	public abstract class TradingStrategy<T> : ITradingStrategy where T : TradingStrategySettings, new()
	{
		protected ITradingSession session;
		protected ITradingPeriodAggregator aggregator;
		protected ITradeSymbol symbol;
		protected Dictionary<int, IList<IIndicator>> indicators = new Dictionary<int, IList<IIndicator>>();
		protected T settings = new T();

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

		public virtual object Settings
		{
			get { return settings; }
			set
			{
				if (settings == null) return;

				dynamic obj = value;
				settings.Capacity = (int)obj.Capacity;				
				settings.PeriodValue = (PeriodValueType)obj.PeriodValue;
				settings.ReversePositions = obj.ReversePositions;
				settings.PeriodCorrection = obj.PeriodCorrection;
				settings.TickPercentage = obj.TickPercentage;

				//settings.PeriodTicks.Clear();
				//settings.PeriodTicks.AddRange((IList<int>)obj.PeriodTicks);
				settings.PeriodTicks = (int[])obj.PeriodTicks;
			}
		}

		public virtual Type SettingsType
		{
			get { return typeof(T); }
		}

		public TradingStrategy()
		{
			session = new TradingSession();
			aggregator = new TradingPeriodAggregator();

			aggregator.AfterNewPeriod += AfterNewPeriod;
			aggregator.BeforeNewPeriod += BeforeNewPeriod;
		}

		public virtual void AddQuote(ITradeQuote quote)
		{
			aggregator.AddQuote(quote);
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

		protected void Reverse(TradeType type)
		{
			if (session.Position == 0)
			{
				if (type == TradeType.Buy || (type == TradeType.Sell && settings.ReversePositions))
				{
					session.Buy(aggregator);
				}
				else
				{
					session.Sell(aggregator);
				}
			}
			else
			{
				session.Reverse(aggregator);
			}
		}
	}
}
