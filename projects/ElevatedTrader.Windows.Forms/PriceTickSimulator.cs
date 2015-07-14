using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace ElevatedTrader.Windows.Forms
{
	public class PriceTickSimulator
	{

		public IList<double> Differences
		{
			get;
			set;
		}

		public double Price
		{
			get;
			set;
		}

		public void Step()
		{
			Price += Differences[Math.Abs(MathNet.Numerics.Random.RandomSeed.Robust()) % Differences.Count];
		}
	}
}
