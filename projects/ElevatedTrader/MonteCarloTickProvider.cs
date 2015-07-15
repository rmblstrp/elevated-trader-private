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
			
			//movement = new Normal(0, 1, new CryptoRandomSource());
			//shock = new Normal(0, 1, new CryptoRandomSource());
			movement = new Normal(0, 1);
			shock = new Normal(0, 1);

			Reset();
			
		}

		public TickProviderResult Next()
		{
			var drift = Symbol.TickVariance * movement.Sample();
			var volatility = Symbol.TickDeviation * shock.Sample();
			var delta = Math.Round(drift + volatility);
			//var delta = Math.Round(volatility);

			price += delta * Symbol.TickRate;

			tick.Price = price;
			tick.Bid = price - Symbol.QuoteSpreadTicks;
			tick.Ask = price + Symbol.QuoteSpreadTicks;

			return TickProviderResult.Ticked;
		}

		public void Reset()
		{
			price = Symbol.CurrentPrice;
		}
	}
}
