using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeResult
	{
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
