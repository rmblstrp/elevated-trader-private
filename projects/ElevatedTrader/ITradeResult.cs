﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeResult
	{
		DateTime Executed
		{
			get;
			set;
		}

		int Quantity
		{
			get;
			set;
		}

		double Price
		{
			get;
			set;
		}

		double Fees
		{
			get;
			set;
		}
	}
}
