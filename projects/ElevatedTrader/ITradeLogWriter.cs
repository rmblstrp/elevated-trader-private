using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeLogWriter
	{
		void WriteTrade(ITradeEntry trade);
	}
}
