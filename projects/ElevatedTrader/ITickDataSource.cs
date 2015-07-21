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

		Type ConfigurationType
		{
			get;
		}

		IList<ITick> Ticks
		{
			get;
		}

		event EventHandler<ITick> TickAdded;

		void Clear();

		void Configure(object configuration);

		void Load(string symbol, int? count = null);
	}
}
