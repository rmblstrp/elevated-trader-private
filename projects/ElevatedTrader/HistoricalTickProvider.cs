using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class HistoricalTickProvider : ITickProvider
	{
		private int index = -1;
		private ITickDataSource source;

		public ITickDataSource DataSource
		{
			get { return source; }
			set { source = value; }
		}

		public ITick Tick
		{
			get { return source.Ticks[index]; }
		}		

		public TickProviderResult Next()
		{
			index++;			

			return index < source.Ticks.Count ? TickProviderResult.Ticked : TickProviderResult.Done;
		}

		public void Initialize()
		{
			index = -1;
		}
	}
}
