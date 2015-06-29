using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class HullStrategy : TradingStrategy
{
	public class StrategySettings
	{
		private int capacity = 1000000;
		private int length = 8;		

		public int Capcity
		{
			get { return capacity; }
			set { capacity = value; }
		}

		public int Length
		{
			get { return length; }
			set { length = value; }
		}
	}

	private StrategySettings settings = new StrategySettings();

	public override object Settings
	{
		get { return settings; }
	}
}
