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
			private Normal distribution = new Normal(0, 1);

			public double Price
			{
				get;
				set;
			}

			public double Mean
			{
				get;
				set;
			}

			public double Interval
			{
				get;
				set;
			}

			public double TickRate
			{
				get;
				set;
			}

			public double TickDeviation
			{
				get;
				set;
			}

			public void Step()
			{
				var movement = Math.Round(TickDeviation * distribution.Sample()) * TickRate;
				Price += movement;
			}
		}
}
