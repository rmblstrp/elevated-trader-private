using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class LiveTickProvider : ITickProvider
	{
		private readonly Queue<ITick> tickQueue = new Queue<ITick>();
		protected virtual Queue<ITick> TickQueue
		{
			get { return tickQueue; }
		}

		public virtual ITick Tick
		{
			get;
			protected set;
		}

		public virtual ITickDataSource DataSource
		{
			get;
			set;
		}

		public virtual TickProviderResult Next()
		{
			if (TickQueue.Count == 0)
			{
				LoadTicks();
			}

			Tick = TickQueue.Dequeue();

			return Tick == null ? TickProviderResult.None : TickProviderResult.Ticked;
		}

		public virtual void Initialize()
		{

		}

		protected virtual void LoadTicks()
		{
			var tick_list = DataSource.Ticks;

			foreach (var item in tick_list)
			{
				TickQueue.Enqueue(item);
			}

			tick_list.Clear();
		}
	}
}
