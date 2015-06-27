﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeSymbol
	{
		string Symbol
		{
			get;
			set;
		}

		double TickRate
		{
			get;
			set;
		}

		double TickValue
		{
			get;
			set;
		}

		int Slippage
		{
			get;
			set;
		}
	}
}
