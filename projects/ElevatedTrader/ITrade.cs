using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITrade
	{
		TradeType Type
		{
			get;
			private set;
		}

		int Quantity
		{
			get;
			private set;
		}

		double Price
		{
			get;
			set;
		}
	}
}
