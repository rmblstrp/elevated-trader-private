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
		protected List<double> quotes;
		protected double total, quoteTotal;

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

		public virtual double EfficiencyRatio
		{
			get
			{
				return Math.Abs(Close - Open) / changes.Sum(x => Math.Abs(x));
			}
		}

		public virtual IList<double> Quotes
		{
			get { return quotes; }
		}

		public virtual IList<double> Ticks
		{
			get { return ticks; }
		}

		public virtual IList<double> Changes
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
			quotes = new List<double>(capacity * 2);
		}

		public virtual void AddQuote(ITradeQuote quote)
		{
			var average = (quote.Bid - quote.Ask) / 2;

			quoteTotal += average;
			quotes.Add(average);
		}

		public virtual void AddTick(ITradeTick tick)
		{
			if (TickCount == 0)
			{
				Open = High = Low = Close = tick.Price;
				changes.Add(0);
			}
			else
			{
				changes.Add(tick.Price - Close);
				Close = tick.Price;
				High = Math.Max(High, tick.Price);
				Low = Math.Min(Low, tick.Price);
			}

			TickCount++;
			total += tick.Price;

			ticks.Add(tick.Price);
		}

		public virtual double QuoteValue(PeriodValueType type)
		{
			switch (type)
			{
				case PeriodValueType.Average:
					return quoteTotal / quotes.Count;
				case PeriodValueType.GeometricMean:
					return MathHelper.GeometricMean(quotes);
				case PeriodValueType.WeightedAverage:
					return MathHelper.WeightedAverage(quotes);
				case PeriodValueType.HarmonicMean:
					return MathHelper.HarmonicMean(quotes);
				case PeriodValueType.Median:
					return Statistics.Median(quotes);
				case PeriodValueType.Skewness:
					return Statistics.Skewness(quotes);
				case PeriodValueType.Variance:
					return Statistics.Variance(quotes);
				case PeriodValueType.Kurtosis:
					return Statistics.Kurtosis(quotes);
				case PeriodValueType.StandardDeviation:
					return Statistics.StandardDeviation(quotes);
			}

			return 0;
		}

		public virtual double PeriodValue(PeriodValueType type)
		{
			switch (type)
			{
				case PeriodValueType.Average:
					return total / TickCount;
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
