using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;

namespace ElevatedTrader
{
	public class TradingPeriod : ITradingPeriod
	{
		protected List<double> ticks;
		protected List<double> changes;

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

		public virtual double EfficiencyRatio
		{
			get
			{
				return Math.Abs(Close - Open) / changes.Sum(x => Math.Abs(x));
			}
		}

		public IList<double> Ticks
		{
			get { return ticks; }
		}

		public IList<double> Changes
		{
			get { return changes; }
		}

		public TradingPeriod()
			: this(1000)
		{
		}

		public TradingPeriod(int capacity)
		{
			ticks = new List<double>(capacity);
			changes = new List<double>(capacity);
		}

		public void AddTick(ITradeTick tick)
		{
			if (TickCount == 0)
			{
				Open = High = Low = Close = tick.Last;
				changes.Add(0);
			}
			else
			{
				changes.Add(tick.Last - Close);
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
			switch (type)
			{
				case PriceType.Average:
					return Total / TickCount;
				case PriceType.GeometricMean:
					return MathHelper.GeometricMean(ticks);
				case PriceType.WeightedAverage:
					return MathHelper.WeightedAverage(ticks);
				case PriceType.HarmonicMean:
					return MathHelper.HarmonicMean(ticks);
				case PriceType.Median:
					return Statistics.Median(ticks);
				case PriceType.Skewness:
					return Statistics.Skewness(ticks);
				case PriceType.Variance:
					return Statistics.Variance(ticks);
				case PriceType.Kurtosis:
					return Statistics.Kurtosis(ticks);
				case PriceType.StandardDeviation:
					return Statistics.StandardDeviation(ticks);
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
