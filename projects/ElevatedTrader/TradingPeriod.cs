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

		public double Value(PeriodValueType type)
		{
			switch (type)
			{
				case PeriodValueType.Average:
					return Total / TickCount;
				case PeriodValueType.GeometricMean:
					return MathHelper.GeometricMean(ticks);
				case PeriodValueType.WeightedAverage:
					return MathHelper.WeightedAverage(ticks);
				case PeriodValueType.HarmonicMean:
					return MathHelper.HarmonicMean(ticks);
				case PeriodValueType.Median:
					return Statistics.Median(ticks);
				case PeriodValueType.Skewness:
					return Statistics.Skewness(ticks);
				case PeriodValueType.Variance:
					return Statistics.Variance(ticks);
				case PeriodValueType.Kurtosis:
					return Statistics.Kurtosis(ticks);
				case PeriodValueType.StandardDeviation:
					return Statistics.StandardDeviation(ticks);
			}

			var count = (double)0;
			var sum = (double)0;

			if ((type & PeriodValueType.Open) == PeriodValueType.Open)
			{
				count++;
				sum += Open;
			}

			if ((type & PeriodValueType.High) == PeriodValueType.High)
			{
				count++;
				sum += High;
			}

			if ((type & PeriodValueType.Low) == PeriodValueType.Low)
			{
				count++;
				sum += Low;
			}

			if ((type & PeriodValueType.Close) == PeriodValueType.Close)
			{
				count++;
				sum += Close;
			}

			return sum / count;
		}
	}
}
