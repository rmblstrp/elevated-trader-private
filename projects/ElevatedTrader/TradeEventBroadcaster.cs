using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeEventBroadcaster : IDisposable
	{
		private HashSet<ITradeEventReceiver> subscribers = new HashSet<ITradeEventReceiver>();
		private HashSet<ITradingSession> sessions = new HashSet<ITradingSession>();

		private delegate void TradeEvent(ITradeEntry trade);
		private event TradeEvent Trade;

		public void Attach(ITradeEventReceiver receiver)
		{
			if (!subscribers.Contains(receiver))
			{
				AttachEvents(receiver);
				subscribers.Add(receiver);
			}
		}

		public void Attach(ITradingSession session)
		{
			if (!sessions.Contains(session))
			{
				session.Trade += SessionTrade;
				sessions.Add(session);
			}
		}

		void SessionTrade(object sender, ITradeEntry trade)
		{
			if (Trade != null)
			{
				Trade(trade);
			}
		}

		public void Detach(ITradeEventReceiver receiver)
		{
			if (subscribers.Contains(receiver))
			{
				DetachEvents(receiver);
				subscribers.Remove(receiver);
			}
		}

		public void Detach(ITradingSession session)
		{
			if (sessions.Contains(session))
			{
				session.Trade += SessionTrade;
				sessions.Remove(session);
			}
		}

		protected void AttachEvents(ITradeEventReceiver receiver)
		{
			Trade += receiver.Trade;
		}

		protected void AttachEvents(ITradingSession session)
		{
			session.Trade += SessionTrade;
		}

		protected void DetachEvents(ITradeEventReceiver receiver)
		{
			Trade -= receiver.Trade;
		}

		protected void DetachEvents(ITradingSession session)
		{
			session.Trade -= SessionTrade;
		}

		public void Dispose()
		{
			foreach (var item in subscribers)
			{
				try
				{
					DetachEvents(item);
				}
				catch { }
			}

			foreach (var item in sessions)
			{
				try
				{
					DetachEvents(item);
				}
				catch { }
			}

			subscribers.Clear();
			subscribers = null;

			sessions.Clear();
			sessions = null;
		}
	}
}
