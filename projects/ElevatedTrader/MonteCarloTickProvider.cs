using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace ElevatedTrader
{
	public class MonteCarloTickProvider : ITickProvider
	{
		private double price;
		private Tick tick;
		private CryptoRandomSource random = new CryptoRandomSource();
		private int index = 0, length = 0, count = 0;
		private ITickDataSource source;

		public ITickDataSource DataSource
		{
			get { return source; }
			set { source = value; }
		}

		public ITick Tick
		{
			get { return tick; }
		}

		public void Initialize()
		{
			price = source.Ticks.First().Price;
		}

		public TickProviderResult Next()
		{
			RandomTicks();

			return TickProviderResult.Ticked;
		}

		protected void RandomTicks()
		{
			if (count == length)
			{
				index = random.Next(source.Deltas.Count - 1);
				length = random.Next((source.Deltas.Count / 10) - 1);

				count = 0;
			}

			var item = source.Deltas[index];

			price += item.Price;

			tick.Price = price;
			tick.Bid = price - item.Bid;
			tick.Ask = price - item.Ask;

			index = ++index % source.Deltas.Count;
			count++;
		}
	}
}
