using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeLogger : ITradeEventReceiver
	{
		private HashSet<ITradeLogWriter> writers = new HashSet<ITradeLogWriter>();

		public void Trade(ITradeEntry trade)
		{
			
		}

		public void Attach(ITradeLogWriter writer)
		{
			if (!writers.Contains(writer))
			{
				writers.Add(writer);
			}
		}

		public void Detach(ITradeLogWriter writer)
		{
			if (writers.Contains(writer))
			{
				writers.Remove(writer);
			}
		}
	}
}
