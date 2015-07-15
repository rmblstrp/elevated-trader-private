using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class HistoricalTickProvider : ITradeTickProvider
	{
		private IList<ITradeTick> ticks;

		private int index = 0;

		public ITradeSymbol Symbol
		{
			get;
			protected set;
		}

		public ITradeTick Tick
		{
			get { return ticks[index]; }
		}

		public void Initialize(ITradeSymbol symbol, IList<ITradeTick> ticks = null)
		{
			Symbol = symbol;
			this.ticks = ticks;
		}

		public TickProviderResult Next()
		{
			index++;

			return index < ticks.Count ? TickProviderResult.Ticked : TickProviderResult.Done;
		}

		public void Reset()
		{
			index = 0;
		}
	}
}
