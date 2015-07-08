using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingStrategySettings
	{
		protected int[] ticks = new int[1];
		protected int capacity = 8192;
		protected PeriodValueType valueType = PeriodValueType.WeightedAverage;
		private double tickPercentage = 0.75;
		private bool periodCorrection = false;

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
