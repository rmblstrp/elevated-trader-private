using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITickDataSource
	{
		IList<ITick> Ticks
		{
			get;
		}

		void Clear();

		void Configure(dynamic configuration);

		void Load(string symbol, int? count = null, Func<ITick, bool> added = null);
	}
}
