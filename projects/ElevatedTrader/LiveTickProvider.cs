using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class LiveTickProvider : ITickProvider
	{
		protected ITickDataSource dataSource;
		protected readonly Queue<ITick> tickQueue = new Queue<ITick>();
		protected ITick tick;

		public ITick Tick
		{
			get { return tick; }
		}

		public ITickDataSource DataSource
		{
			get { return dataSource; }
			set { dataSource = value; }
		}

		public TickProviderResult Next()
		{
			if (tickQueue.Count == 0)
			{
				LoadTicks();
			}

			tick = tickQueue.Dequeue();

			return tick == null ? TickProviderResult.None : TickProviderResult.Ticked;
		}

		public void Initialize()
		{

		}

		protected void LoadTicks()
		{
			var tick_list = dataSource.Ticks;

			foreach (var item in tick_list)
			{
				tickQueue.Enqueue(item);
			}

			tick_list.Clear();
		}
	}
}
