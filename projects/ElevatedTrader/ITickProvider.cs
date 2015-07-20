using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITickProvider
	{
		ITick Tick
		{
			get;
		}

		ITickDataSource DataSource
		{
			get;
			set;
		}

		TickProviderResult Next();

		void Initialize();
	}
}
