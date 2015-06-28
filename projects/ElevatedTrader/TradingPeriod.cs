using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingPeriod : ITradingPeriod
	{
		public virtual int TickCount
		{
			get;
			protected set;
		}

		public virtual double Open
		{
			get;
			protected set;
		}

		public virtual double High
		{
			get;
			protected set;
		}

		public virtual double Low
		{
			get;
			protected set;
		}

		public virtual double Close
		{
			get;
			protected set;
		}

		public virtual double Total
		{
			get;
			protected set;
		}

		public void AddTick(ITradeTick tick)
		{
			throw new NotImplementedException();
		}
	}
}
