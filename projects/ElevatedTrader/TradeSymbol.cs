﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeSymbol : ITradeSymbol
	{
		public string Symbol
		{
			get;
			set;
		}

		public double TickRate
		{
			get;
			set;
		}

		public double TickValue
		{
			get;
			set;
		}

		public int Slippage
		{
			get;
			set;
		}
	}
}
