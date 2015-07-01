using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class HullStrategy : TradingStrategy
{
	private HullMovingAverage hma;

	public class StrategySettings
	{
		private int capacity = 8192;
		private int length = 8;
		private int ticks = 610;

		public int Capacity
		{
			get { return capacity; }
			set { capacity = value; }
		}

		public int Length
		{
			get { return length; }
			set { length = value; }
		}

		public int Ticks
		{
			get { return ticks; }
			set { ticks = value; }
		}
	}

	private StrategySettings settings = new StrategySettings();

	public override object Settings
	{
		get { return settings; }
	}

	public override void AddTick(ITradeTick tick)
	{
		base.AddTick(tick);

		hma.Calculate(periods);
	}

	protected override void BeforeNewPeriod(int size)
	{
		base.BeforeNewPeriod(size);

		hma.NewPeriod();
	}

	public override void Initialize()
	{
		base.Initialize();

		periods.AddSize(settings.Ticks, settings.Capacity);
		hma = new HullMovingAverage(settings.Capacity)
		{
			Length = settings.Length
		};
	}
}
