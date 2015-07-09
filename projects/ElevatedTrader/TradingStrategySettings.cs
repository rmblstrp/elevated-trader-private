using System;
using System.Linq;

namespace ElevatedTrader
{
	public class TradingStrategySettings
	{
		protected int[] ticks = new int[] { 610 };
		protected int capacity = 10000;
		protected PeriodValueType valueType = PeriodValueType.Close;		
		protected bool periodCorrection = false;
		protected double tickPercentage = 0.75;

		public int Capacity
		{
			get { return capacity; }
			set { capacity = value; }
		}

		public int[] PeriodTicks
		{
			get { return ticks; }
			set { ticks = value; }
		}

		public PeriodValueType PeriodValue
		{
			get { return valueType; }
			set { valueType = value; }
		}

		public bool ReversePositions
		{
			get;
			set;
		}

		public bool PeriodCorrection
		{
			get { return periodCorrection; }
			set { periodCorrection = value; }
		}

		public double TickPercentage
		{
			get { return tickPercentage; }
			set { tickPercentage = value; }
		}
	}
}
