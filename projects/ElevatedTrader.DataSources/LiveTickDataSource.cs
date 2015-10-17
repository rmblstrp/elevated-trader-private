using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader.DataSources
{
	public class LiveTickDataSource : ITickDataSource, ITickReceiver
	{
		private readonly ConcurrentQueue<ITick> tickQueue = new ConcurrentQueue<ITick>();		

		#region -- ITickDataSource --
		public event Action TicksAdded;

		public IList<ITick> Ticks
		{
			get { return GetTicks(); }
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public void Configure(dynamic configuration)
		{
			throw new NotImplementedException();
		}

		public void Load(string symbol, int? count = null, Func<ITick, bool> added = null)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region -- ITickReceiver --
		public void TickReceived(object sender, ITick tick)
		{
			try
			{
				tickQueue.Enqueue(tick);
				DoTicksAdded();
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		#endregion

		protected IList<ITick> GetTicks()
		{
			List<ITick> list = new List<ITick>(tickQueue.Count);
			ITick tick;

			while (tickQueue.TryDequeue(out tick))
			{
				list.Add(tick);
				tick = null;
			}

			return list;
		}

		protected void DoTicksAdded()
		{
			if (TicksAdded != null)
			{
				TicksAdded();
			}
		}
	}
}
