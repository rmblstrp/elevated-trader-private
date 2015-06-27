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
		}

		int Quantity
		{
			get;
		}

		double Price
		{
			get;
		}

		IDictionary<int, int> Indicies { get; }
	}
}
