using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace ElevatedTrader
{
	public interface ISymbolIndicator
	{
		IList<ISymbolIndicatorResult> Results { get; }

		bool IsStockPriceRelated { get; }

		void Calculate(IList<ITradingPeriod> periods);

		void AfterNewPeriod();

		void Clear();

		void FreeResources();
	}

	public interface ISymbolIndicator<T> : ISymbolIndicator where T : TradingStrategySettings, new()
	{
		T Settings { get; set; }
	}
}
