using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ElevatedTrader.LiveTrading.Service
{
	public class TradeDataLink : ElevatedTrader.DevExperts.TradeDataLink
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		protected override void ListenerTimeAndSale(object sender, TradeHistoryTimeAndSale obj)
		{
			try
			{
				base.ListenerTimeAndSale(sender, obj);
			}
			catch (Exception ex)
			{
				logger.Error<Exception>(ex);
			}
		}
	}
}
