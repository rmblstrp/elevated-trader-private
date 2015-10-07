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
	public class EventListener : IDxFeedListener, IDisposable
	{
		public void OnFundamental<TB, TE>(TB buf)
			where TB : IDxEventBuf<TE>
			where TE : IDxSummary
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
					Type = (int)item.Type
				};
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

			}
		}
	}
}
