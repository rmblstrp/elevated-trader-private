﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatedTrader.Windows.Forms
{
	public partial class MainForm : Form
	{
		private ITradingStrategy strategy = new HullStrategy();
		private ITradeSymbol symbol = new TradeSymbol();

		public MainForm()
		{
			InitializeComponent();

			SymbolProperties.SelectedObject = symbol;
			StrategySettings.SelectedObject = strategy.Settings;
		}
	}
}
