﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ISymbolIndicatorResult
	{
		TrendDirection Direction
		{
			get;
		}

		bool Signaled
		{
			get;
		}

		IList<double> Values
		{
			get;
		}
	}
}