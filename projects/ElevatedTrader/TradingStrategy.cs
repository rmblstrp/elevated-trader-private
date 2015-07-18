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
		protected Dictionary<int, IList<IIndicator>> indicators = new Dictionary<int, IList<IIndicator>>();
		protected T settings = new T();
		protected Dictionary<int, bool> periodTriggered = new Dictionary<int, bool>();

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

				if (obj.PeriodTicks as IEnumerable<object> != null)
				{
					var sizes = ((IEnumerable<object>)obj.PeriodTicks);
					settings.PeriodTicks = (from x in sizes select Convert.ToInt32(x)).ToArray();
				}
				else
				{
					settings.PeriodTicks = (int[])obj.PeriodTicks;
				}
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

			for (int index = 0; index < settings.PeriodTicks.Length; index++)
			{
				var size = settings.PeriodTicks[index];
				var period = aggregator.Periods[size].Last();
				var triggered = periodTriggered[size];

				if (!triggered && period.TickCount >= size * settings.TickPercentage)
				{
					OnPeriodTrigger(size);
					periodTriggered[size] = true;
				}
			}
		}

		protected virtual void AfterNewPeriod(int size)
		{
			periodTriggered[size] = false;
		}

		protected virtual void BeforeNewPeriod(int size)
		{
		}

		protected virtual void OnPeriodTrigger(int size)
		{
		}

		public virtual void FreeResources()
		{
		}

		public virtual void Clear()
		{
			session.Reset();
			aggregator.Clear();
			indicators.Clear();
			periodTriggered.Clear();
		}

		public virtual void Initialize()
		{
			Clear();

			for (int index = 0; index < settings.PeriodTicks.Length; index++)
			{
				var size = settings.PeriodTicks[index];
				aggregator.AddSize(size, settings.Capacity);
				periodTriggered.Add(size, false);
			}
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
			else if (type == TradeType.Buy && session.Position < 0 || type == TradeType.Sell && session.Position > 0)
			{
				session.Reverse(aggregator);
			}
		}
	}
}
