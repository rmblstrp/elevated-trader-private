using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace ElevatedTrader
{
	public class MonteCarloTickProvider : ITradeTickProvider
	{
		protected double price;
		protected Normal movement;
		protected Normal shock;
		protected TradeTick tick;
		protected Action internalNext;
		protected Random random = new Random();
		protected TickData[] tickData;
		protected int index = 0;

		protected struct TickData
		{
			public double Delta;
			public double Bid;
			public double Ask;
		}

		public ITradeSymbol Symbol
		{
			get;
			protected set;
		}

		public ITradeTick Tick
		{
			get { return tick; }
		}

		public void Initialize(ITradeSymbol symbol, IList<ITradeTick> ticks = null)
		{
			Symbol = symbol;
			tick = new TradeTick();

			if (ticks == null || ticks.Count < 100000)
			{
				//movement = new Normal(0, 1, new CryptoRandomSource());
				//shock = new Normal(0, 1, new CryptoRandomSource());
				movement = new Normal(Symbol.TickMean, Symbol.TickDeviation);
				shock = new Normal(Symbol.TickMean, Symbol.TickDeviation);

				internalNext = GenerateTicks;
			}
			else
			{
				tickData = new TickData[ticks.Count - 1];

				for (int index = 1; index < ticks.Count; index++)
				{
					var previous = ticks[index - 1];
					var current = ticks[index];

					var delta = current.Price - previous.Price;
					var bid = current.Price - current.Ask;
					var ask = current.Ask - current.Price;

					tickData[index - 1] = new TickData()
					{
						Delta = delta,
						Bid = bid,
						Ask = ask
					};
				}

				Shuffle<TickData>(tickData);

				internalNext = ShuffleTicks;
			}

			Reset();

		}

		public TickProviderResult Next()
		{
			internalNext();

			return TickProviderResult.Ticked;
		}

		protected void ShuffleTicks()
		{
			var item = tickData[index++];

			price += item.Delta;

			tick.Price = price;
			tick.Bid = price - item.Bid;
			tick.Ask = price + item.Ask;

			index = index % tickData.Length;

			if (index == 0)
			{
				Shuffle<TickData>(tickData);
			}
		}

		protected void GenerateTicks()
		{
			var drift = movement.Sample() * Symbol.TickRate;
			var volatility = shock.Sample() / Symbol.TickRate;
			var delta = Math.Round(drift + volatility);
			//var delta = Math.Round(volatility);

			price += delta * Symbol.TickRate;

			tick.Price = price;
			tick.Bid = price - Symbol.QuoteSpreadTicks;
			tick.Ask = price + Symbol.QuoteSpreadTicks;
		}

		public void Shuffle<T>(T[] array)
		{
			int n = array.Length;
			while (n > 1)
			{
				int k = random.Next(n--);
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}

		public void Reset()
		{
			price = Symbol.CurrentPrice;
		}
	}
}
