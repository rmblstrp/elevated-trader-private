﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingPeriodAggregator
	{
		ITradeTick Last { get; }

		IDictionary<int, IList<ITradingPeriod>> Periods { get; }

		event Action<int> AfterNewPeriod;

		event Action<int> BeforeNewPeriod;

		void AddSize(int size, int capacity);

		void AddQuote(ITradeQuote quote);

		void AddTick(ITradeTick tick);

		ITradingPeriod CurrentPeriod(int size);

		IDictionary<int, int> Indexes();

		void Clear();
	}
}
