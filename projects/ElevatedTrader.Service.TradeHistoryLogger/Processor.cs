using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using com.dxfeed.api;
using com.dxfeed.native;

namespace ElevatedTrader.Service.TradeHistoryLogger
{
	class Processor
	{
		class EventListener : IDxFeedListener, IDisposable
		{
			private SqlConnection connection;

			public EventListener(string connection_string)
			{
				connection = new SqlConnection(connection_string);
				connection.Open();
			}

			public void Dispose()
			{
				connection.Close();
				connection.Dispose();
			}

			public void OnFundamental<TB, TE>(TB buf)
				where TB : IDxEventBuf<TE>
				where TE : IDxFundamental
			{

			}

			public void OnOrder<TB, TE>(TB buf)
				where TB : IDxEventBuf<TE>
				where TE : IDxOrder
			{
				foreach (var item in buf)
				{
					var obj = new TradeHistoryOrder()
					{
						ExchangeCode = item.ExchangeCode,
						Index = item.Index,
						Level = item.Level,
						MarketMaker = item.MarketMaker.ToString(),
						Price = item.Price,
						Side = (TradeHistoryOrder.OrderSide)item.Side,
						Size = item.Size,
						Time = item.Time
					};

					LogEvent<TradeHistoryOrder>(TradeHistoryType.Order, buf.Symbol.ToString(), obj);
				}
			}

			public void OnProfile<TB, TE>(TB buf)
				where TB : IDxEventBuf<TE>
				where TE : IDxProfile
			{

			}

			public void OnQuote<TB, TE>(TB buf)
				where TB : IDxEventBuf<TE>
				where TE : IDxQuote
			{

				foreach (var item in buf)
				{
					var obj = new TradeHistoryQuote()
					{
						AskExchangeCode = item.AskExchangeCode,
						AskPrice = item.AskPrice,
						AskSize = item.AskSize,
						AskTime = item.AskTime,
						BidExchangeCode = item.BidExchangeCode,
						BidPrice = item.BidPrice,
						BidSize = item.BidSize,
						BidTime = item.BidTime
					};

					LogEvent<TradeHistoryQuote>(TradeHistoryType.Quote, buf.Symbol.ToString(), obj);
				}
			}

			public void OnTimeAndSale<TB, TE>(TB buf)
				where TB : IDxEventBuf<TE>
				where TE : IDxTimeAndSale
			{
				foreach (var item in buf)
				{
					var obj = new TradeHistoryTimeAndSale()
					{
						AskPrice = item.AskPrice,
						BidPrice = item.BidPrice,
						EventId = item.EventId,
						ExchangeCode = item.ExchangeCode,
						ExchangeSaleConditions = item.ExchangeSaleConditions.ToString(),
						IsTrade = item.IsTrade,
						Price = item.Price,
						Size = item.Size,
						Time = item.Time,
						Type = item.Type
					};

					LogEvent<TradeHistoryTimeAndSale>(TradeHistoryType.TimeAndSale, buf.Symbol.ToString(), obj);
				}
			}

			public void OnTrade<TB, TE>(TB buf)
				where TB : IDxEventBuf<TE>
				where TE : IDxTrade
			{
				foreach (var item in buf)
				{
					var obj = new TradeHistoryTrade()
					{
						DayVolume = item.DayVolume,
						ExchangeCode = item.ExchangeCode,
						Price = item.Price,
						Size = item.Size,
						Time = item.Time
					};

					LogEvent<TradeHistoryTrade>(TradeHistoryType.Trade, buf.Symbol.ToString(), obj);
				}
			}

			private void LogEvent<T>(TradeHistoryType type, string symbol, T history)
			{
				var json = Newtonsoft.Json.JsonConvert.SerializeObject(history);

				try
				{
					using (var command = connection.CreateCommand())
					{
						command.CommandText = "insert into SymbolHistory (symbol, type, json) values (@symbol, @type, @json)";
						command.Parameters.Add(new SqlParameter("@symbol", symbol));
						command.Parameters.Add(new SqlParameter("@type", (int)type));
						command.Parameters.Add(new SqlParameter("@json", json));
						command.ExecuteNonQuery();

						logger.Info(string.Format("{0} - {2}: {1}", typeof(T).Name, json, symbol));
					}
				}
				catch (Exception ex)
				{
					logger.Error(string.Format("{0} - {2}: {1}", typeof(T).Name, ex.Message, symbol));
				}
			}
		}

		private static readonly Logger logger = LogManager.GetCurrentClassLogger();
		private NativeConnection feed;
		private IDxSubscription subscription;
		private EventListener listener;

		public void Start()
		{
			try
			{
				var host = ConfigurationManager.AppSettings["dxfeed"];

				logger.Info("Connecting to feed: " + host);
				feed = new NativeConnection();
				feed.Connect(host);

				logger.Info("Creating event listener");
				listener = new EventListener(ConfigurationManager.ConnectionStrings["trader"].ConnectionString);

				logger.Info("Creating feed subscription");
				subscription = feed.CreateSubscription(EventType.Quote | EventType.Trade | EventType.TimeAndSale, listener);
				subscription.SetSymbols(ConfigurationManager.AppSettings["symbols"].Split(','));
			}
			catch (Exception ex)
			{
				logger.Fatal(ex.Message);

				throw;
			}
		}

		public void Stop()
		{
			if (subscription != null)
			{
				logger.Info("Disposing of feed subscription");
				subscription.Dispose();
				subscription = null;
			}

			if (feed != null)
			{
				logger.Info("Closing feed connection");
				feed.Disconnect();
				feed.Dispose();
				feed = null;
			}
		}
	}
}
