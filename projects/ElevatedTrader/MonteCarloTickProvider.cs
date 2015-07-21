using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace ElevatedTrader
{
	public class MonteCarloTickProvider : ITickProvider, IDisposable
	{
		private double price;
		private Tick tick = new Tick();
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

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					random.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~MonteCarloTickProvider() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
