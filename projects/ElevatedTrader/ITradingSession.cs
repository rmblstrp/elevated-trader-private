using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		ITradingPeriodAggregator PeriodAggregator
		{
			get;
			set;
		}

		double Equity
		{
			get;
		}

		IFinancialInstrument Instrument
		{
			get;
			set;
		}

		int Position
		{
			get;
		}

		IList<ITradeEntry> Trades { get; }

		event EventHandler<ITradeEntry> Trade;

		void Buy(int quantity = 1);
		void Sell(int quantity = 1);
		void Reverse();

		void ClosePosition();

		void Reset();
	}
}
