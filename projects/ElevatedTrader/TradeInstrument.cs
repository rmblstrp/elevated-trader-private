﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeInstrument : IFinancialInstrument
	{
		private string symbol;

		public string Symbol
		{
			get { return symbol; }
			set { symbol = value.ToUpper(); }
		}

		public string Description
		{
			get;
			set;
		}

		public double TickIncrement
		{
			get;
			set;
		}

		public double TickValue
		{
			get;
			set;
		}

		public bool HasOpenCost
		{
			get;
			set;
		}

		public double PerTradeCost
		{
			get;
			set;
		}

		public double PerQuantityCost
		{
			get;
			set;
		}

		public int Slippage
		{
			get;
			set;
		}

		public override string ToString()
		{
			return string.Format("{0} - {1}", Symbol, string.IsNullOrWhiteSpace(Description) ? "(no description)" : Description);
		}
	}
}
