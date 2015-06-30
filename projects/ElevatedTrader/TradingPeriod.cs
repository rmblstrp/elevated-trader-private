using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingPeriod : ITradingPeriod
	{
		protected List<double> ticks;

		public virtual int TickCount
		{
			get;
			protected set;
		}

		public virtual double Open
		{
			get;
			protected set;
		}

		public virtual double High
		{
			get;
			protected set;
		}

		public virtual double Low
		{
			get;
			protected set;
		}

		public virtual double Close
		{
			get;
			protected set;
		}

		public virtual double Total
		{
			get;
			protected set;
		}

		public IList<double> Ticks
		{
			get { return ticks; }
		}

		public TradingPeriod() : this(1000)
		{
		}

		public TradingPeriod(int capacity)
		{
			ticks = new List<double>(capacity);
		}

		public void AddTick(ITradeTick tick)
		{
			if (TickCount == 0)
			{
				Open = High = Low = Close = tick.Last;
			}
			else
			{
				Close = tick.Last;
				High = Math.Max(High, tick.Last);
				Low = Math.Min(Low, tick.Last);
			}

			TickCount++;
			Total += tick.Last;

			ticks.Add(tick.Last);
		}

		public double Value(PriceType type)
		{
			if (type == PriceType.Average)
			{
				return Total / TickCount;
			}

			var count = (double)0;
			var sum = (double)0;

			if ((type & PriceType.Open) == PriceType.Open)
			{
				count++;
				sum += Open;
			}

			if ((type & PriceType.High) == PriceType.High)
			{
				count++;
				sum += High;
			}

			if ((type & PriceType.Low) == PriceType.Low)
			{
				count++;
				sum += Low;
			}

			if ((type & PriceType.Close) == PriceType.Close)
			{
				count++;
				sum += Close;
			}

			return sum / count;
		}
	}
}
