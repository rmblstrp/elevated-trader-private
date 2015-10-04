using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeEventBroadcaster : IDisposable
	{
		protected HashSet<ITradeEventReceiver> subscribers = new HashSet<ITradeEventReceiver>();

		protected delegate void TradeEvent(ITradeEntry trade);
		protected event TradeEvent trade;

		public virtual void Attach(ITradeEventReceiver receiver)
		{
			if (!subscribers.Contains(receiver))
			{
				AttachEvents(receiver);
				subscribers.Add(receiver);
			}
		}

		public virtual void Detach(ITradeEventReceiver receiver)
		{
			if (subscribers.Contains(receiver))
			{
				DetachEvents(receiver);
				subscribers.Remove(receiver);
			}
		}

		protected virtual void AttachEvents(ITradeEventReceiver receiver)
		{
			trade += receiver.Trade;
		}

		protected virtual void DetachEvents(ITradeEventReceiver receiver)
		{
			trade -= receiver.Trade;
		}

		public virtual void Dispose()
		{
			foreach (var item in subscribers)
			{
				try
				{
					DetachEvents(item);
				}
				catch { }
			}

			subscribers.Clear();
			subscribers = null;
		}
	}
}
