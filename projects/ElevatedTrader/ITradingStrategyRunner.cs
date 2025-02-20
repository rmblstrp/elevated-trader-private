﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingStrategyRunner
	{
		bool LimitResources { get; set; }

		event EventHandler<int> Tick;

		void Run(ITradingStrategy strategy, ITickProvider ticks, int? iterations = null);

		void Stop();
	}
}
