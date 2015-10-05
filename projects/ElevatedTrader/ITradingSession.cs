using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		double Equity
		{
			get;
		}

		ITradeExecutor Executor
		{
			get;
			set;
		}

		IFinancialInstrument Instrument
		{
			get;
			set;
		}

		ITradingPeriodAggregator PeriodAggregator
		{
			get;
			set;
		}

		int Position
		{
			get;
		}

		IList<ITradeEntry> Trades
		{
			get;
		}

		event EventHandler<ITradeEntry> Trade;

		void Buy(int quantity = 1);
		void Sell(int quantity = 1);
		void Reverse();

		void ClosePosition();

		void Reset();
	}
}
