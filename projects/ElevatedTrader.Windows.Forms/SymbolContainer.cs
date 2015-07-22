using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader.Windows.Forms
{
	public class SymbolContainer
	{
		private Dictionary<string, ITickDataSource> dataSources = new Dictionary<string, ITickDataSource>();

		public TradeSymbol Symbol
		{
			get;
			set;
		}

		public Dictionary<string, ITickDataSource> DataSources
		{
			get { return dataSources; }
		}
	}
}
