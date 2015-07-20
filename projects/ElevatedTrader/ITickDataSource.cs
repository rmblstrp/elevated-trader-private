using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITickDataSource
	{
		IList<TickDelta> Deltas
		{
			get;
		}

		int? MaxTicks
		{
			get;
			set;
		}

		IList<ITick> Ticks
		{
			get;
		}

		void Clear();

		void Load();
	}
}
