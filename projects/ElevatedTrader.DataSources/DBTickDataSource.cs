using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader.DataSources
{
	public class DBTickDataSource : ITickDataSource
	{
		const int InitialCapacity = 100000000;

		private List<TickDelta> deltas = new List<TickDelta>(InitialCapacity);
		private List<ITick> ticks = new List<ITick>(InitialCapacity);

		public IList<TickDelta> Deltas
		{
			get { return deltas; }
		}

		public int? MaxTicks
		{
			get;
			set;
		}

		public IList<ITick> Ticks
		{
			get { return ticks; }
		}

		public DBTickDataSource()
		{
		}

		public void Clear()
		{
			deltas.Clear();
			ticks.Clear();
		}

		public void Load()
		{
			
		}
	}
}
