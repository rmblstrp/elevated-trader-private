using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class HistoricalTickProvider : ITickProvider<TickDelta>
	{
		protected IList<TickDelta> deltas;
		protected Tick tick;
		protected double price;

		private int index = 0;

		public ITradeSymbol Symbol
		{
			get;
			protected set;
		}

		public ITick Tick
		{
			get { return tick; }
		}

		public void Initialize()
		{
			throw new NotImplementedException();
		}

		public void Initialize(double price, IList<TickDelta> ticks)
		{
			this.deltas = ticks;
			this.price = price;
		}

		public TickProviderResult Next()
		{
			var item = deltas[index];

			price += item.Price;

			tick.Price = price;
			tick.Bid = price - item.Bid;
			tick.Ask = price - item.Ask;

			index++;			

			return index < deltas.Count ? TickProviderResult.Ticked : TickProviderResult.Done;
		}

		public void Reset()
		{
			index = 0;
		}
	}
}
