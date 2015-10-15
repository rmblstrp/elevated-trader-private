﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public enum TradeExecutionState
	{
		Idle,
		Executing,
		Executed,
		Cancelling,
		Cancelled,
		Failed
	}
}