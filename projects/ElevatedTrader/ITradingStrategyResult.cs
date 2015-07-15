using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingStrategyResult
	{
		IList<ITrade> Trades
		{
			get;
		}
	}
}
