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
		protected HashSet<ITradingSession> sessions = new HashSet<ITradingSession>();

		protected delegate void TradeEvent(ITradeEntry trade);
		protected event TradeEvent Trade;
		protected ITradingSession session;

		public virtual void Attach(ITradeEventReceiver receiver)
		{
			if (!subscribers.Contains(receiver))
			{
				AttachEvents(receiver);
				subscribers.Add(receiver);
			}
		}

		public virtual void Attach(ITradingSession session)
		{
			if (!sessions.Contains(session))
			{
				session.Trade += Session_Trade;
				sessions.Add(session);
			}
		}

		void Session_Trade(object sender, ITradeEntry trade)
		{
			if (Trade != null)
			{
				Trade(trade);
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

		public virtual void Detach(ITradingSession session)
		{
			if (sessions.Contains(session))
			{
				session.Trade += Session_Trade;
				sessions.Remove(session);
			}
		}

		protected virtual void AttachEvents(ITradeEventReceiver receiver)
		{
			Trade += receiver.Trade;
		}

		protected virtual void AttachEvents(ITradingSession session)
		{
			session.Trade += Session_Trade;
		}

		protected virtual void DetachEvents(ITradeEventReceiver receiver)
		{
			Trade -= receiver.Trade;
		}

		protected virtual void DetachEvents(ITradingSession session)
		{
			session.Trade -= Session_Trade;
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
