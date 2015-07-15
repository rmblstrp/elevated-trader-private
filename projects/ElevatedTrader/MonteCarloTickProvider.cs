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
		protected Normal distribution;
		protected TradeTick tick;

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
			
			distribution = new Normal(0, symbol.TickDeviation, new CryptoRandomSource());

			Reset();
			
		}

		public TickProviderResult Next()
		{
			var delta = Symbol.TickDeviation / Symbol.TickRate * distribution.Sample() * Symbol.TickRate;

			price += delta;

			tick.Price = price;
			tick.Bid = price + Symbol.QuoteSpreadTicks;
			tick.Ask = price - Symbol.QuoteSpreadTicks;

			return TickProviderResult.Ticked;
		}

		public void Reset()
		{
			price = Symbol.CurrentPrice;
		}
	}
}
