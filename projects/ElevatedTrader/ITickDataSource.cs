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

		IList<ITick> Ticks
		{
			get;
		}

		void Clear();

		void Configure(string json);

		void Load(int? count = null);
	}
}
