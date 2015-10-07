using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITickBroadcaster
	{
		void Attach(ITickReceiver receiver);
		void Detach(ITickReceiver receiver);
	}
}
