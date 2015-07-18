using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace ElevatedTrader
{
	public class MonteCarloTickProvider : ITradeTickProvider<TickDelta>
	{
		protected double price;
		protected Normal movement;
		protected Normal shock;
		protected TradeTick tick;
		protected Action internalNext;
		//protected Random random = new Random();
		protected CryptoRandomSource random = new CryptoRandomSource();
		protected IList<TickDelta> deltas;
		protected int index = 0, length = 0, count = 0;

		public ITradeSymbol Symbol
		{
			get;
			protected set;
		}

		public ITradeTick Tick
		{
			get { return tick; }
		}

		public void Initialize()
		{
			throw new NotImplementedException();
		}

		public void Initialize(double price, IList<TickDelta> ticks)
		{
			deltas = ticks;
			this.price = price;

			Reset();

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
				index = random.Next(deltas.Count - 1);
				length = random.Next((deltas.Count / 10) - 1);

				count = 0;
			}

			var item = deltas[index];

			price += item.Price;

			tick.Price = price;
			tick.Bid = price - item.Bid;
			tick.Ask = price - item.Ask;

			index = ++index % deltas.Count;
			count++;
		}

		public void Reset()
		{
			
		}
	}
}
