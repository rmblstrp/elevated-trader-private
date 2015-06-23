﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace ElevatedTrader
{
	public interface IIndicator
	{
		List<IIndicatorResult> Results { get; set; }

		void Calculate(IEnumerable<ITradePeriod> periods);

		void Configure(ExpandoObject configuration);

		void FinalizeResult();
	}
}
