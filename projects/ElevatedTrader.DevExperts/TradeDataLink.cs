using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.dxfeed.api;
using com.dxfeed.api.events;
using com.dxfeed.native;

namespace ElevatedTrader.DevExperts
{
	public class TradeDataLink : ITradeDataLink, IConfigurable, ITickBroadcaster, IDisposable
	{
		private readonly HashSet<ITickReceiver> receivers = new HashSet<ITickReceiver>();
		private EventHandler<ITick> tickReceived;
		private readonly List<string> symbols = new List<string>();

		protected virtual HashSet<ITickReceiver> Receivers
		{
			get { return receivers; }
		}

		protected virtual EventHandler<ITick> TickReceived
		{
			get { return tickReceived; }
			set { tickReceived = value; }
		}

		protected virtual NativeConnection Feed
		{
			get;
			set;
		}

		protected virtual IDxSubscription Subscription
		{
			get;
			set;
		}

		protected virtual IDxFeedListener Listener
		{
			get;
			set;
		}

		protected virtual List<string> Symbols
		{
			get { return symbols; }
		}

		protected virtual string Host
		{
			get;
			set;
		}

		public TradeDataLink()
		{
			Listener = new EventListener();
		}

		public void Connect()
		{
			try
			{
				CreateConnection();
			}
			catch (Exception ex)
			{
				// log error
				throw;
			}
		}

		public void Disconnect()
		{
			if (Subscription != null)
			{
				Subscription.Dispose();
				Subscription = null;
			}

			if (Feed != null)
			{
				Feed.Disconnect();
				Feed.Dispose();
				Feed = null;
			}
		}

		public void Configure(dynamic configuration)
		{
			Host = configuration.Host;
			Symbols.AddRange(configuration.Symbols);
		}

		protected void CreateConnection()
		{
			Feed = new NativeConnection(Host, delegate { CreateConnection(); });

			Subscription = CreateSubscription(Feed, Symbols.ToArray());
		}

		protected IDxSubscription CreateSubscription(IDxConnection connection, string[] symbols)
		{
			var subscription = Feed.CreateSubscription(EventType.Quote | EventType.Trade | EventType.TimeAndSale, Listener);
			subscription.SetSymbols(symbols);

			return subscription;
		}

		public virtual void Attach(ITickReceiver receiver)
		{
			if (!receivers.Contains(receiver))
			{
				AttachEvents(receiver);
				Receivers.Add(receiver);
			}
		}

		public virtual void Detach(ITickReceiver receiver)
		{
			if (Receivers.Contains(receiver))
			{
				DetachEvents(receiver);
				Receivers.Remove(receiver);
			}
		}

		protected virtual void AttachEvents(ITickReceiver receiver)
		{
			TickReceived += receiver.TickReceived;
		}

		protected virtual void DetachEvents(ITickReceiver receiver)
		{
			TickReceived -= receiver.TickReceived;
		}

		public virtual void Dispose()
		{
			foreach (var item in Receivers)
			{
				try
				{
					DetachEvents(item);
				}
				catch { }
			}

			Receivers.Clear();

			Disconnect();
		}
	}
}
