using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public static class MathHelper
	{
		public static double GeometricMean(IList<double> values)
		{
			double mean = 1;

			for (int index = 0; index < values.Count; index++)
			{
				mean *= values[index];
			}

			return Math.Pow(mean, 1 / values.Count);
		}

		public static double HullMovingAverage(IList<double> values)
		{
			var length = values.Count / 2;
			var mid = (int)Math.Ceiling((double)length / 2);
			var sqrt = Math.Sqrt(length);
			var difference = new List<double>(length);
			var wma_mid = new List<double>(mid);
			var wma_full = new List<double>(length);

			for (int index = values.Count - length; index < values.Count; index++)
			{
				wma_mid.Clear();
				wma_full.Clear();

				for (int x = 0; x < mid; x++)
				{
					wma_mid.Add(values[index - mid + x]);
				}

				for (int x = 0; x < length; x++)
				{
					wma_full.Add(values[index - length + x]);
				}

				var wma1 = WeightedAverage(wma_mid) * 2;
				var wma2 = WeightedAverage(wma_full);

				difference.Add(wma1 - wma2);
			}

			var hma = new List<double>((int)sqrt);

			for (int index = difference.Count - 1 - (int)sqrt; index < difference.Count; index++)
			{
				hma.Add(difference[index]);
			}

			return WeightedAverage(hma);
		}

		public static double WeightedAverage(IList<double> values)
		{
			var count = values.Count();
			double weighted = 0, sum = 0;

			for (int index = 0; index < count; index++)
			{
				sum += index + 1;
				weighted += (index + 1) * values[index];
			}

			return weighted / sum;
		}

		public static double HarmonicMean(IList<double> values)
		{
			double sum = 0;

			for (int index = 0; index < values.Count; index++)
			{
				sum += 1 / values[index];
			}

			return sum / values.Count;
		}
	}
}
