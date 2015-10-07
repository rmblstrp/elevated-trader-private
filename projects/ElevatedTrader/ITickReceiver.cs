using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITickReceiver
	{
		void TickReceived(object sender, ITick tick);
	}
}
