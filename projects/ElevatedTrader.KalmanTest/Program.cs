using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ElevatedTrader.KalmanTest
{
	class Program
	{
		private string ConnectionStrion = @"Data Source=localhost\sqlexpress;Initial Catalog=AutomatedTrading;Integrated Security=True";

		static void Main(string[] args)
		{
		}

		private void LoadTickData()
		{
			//{"AskPrice":1.111,"BidPrice":1.1109,"EventId":6157792271241579016,"ExchangeCode":"\u0000","IsTrade":true,"Price":1.1109,"Size":1,"Time":"2015-06-08T00:18:58Z","Type":0}
			using (var connection = new SqlConnection(ConnectionStrion))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					//command.CommandText = "select top(@count) json from quotedata where symbol = @symbol";
					command.CommandText = "select top(@count) json, type from symbolhistory where symbol = @symbol order by id asc";
					command.Parameters.Add(new SqlParameter("@symbol", "/ES"));
					command.Parameters.Add(new SqlParameter("@count", 100000));

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							//var item = JsonConvert.DeserializeObject<OldDataFormat>(reader.GetString(0));							

							var type = (TradeHistoryType)reader.GetInt32(1);

							switch (type)
							{
								case TradeHistoryType.Quote:
									var quote = JsonConvert.DeserializeObject<TradeHistoryQuote>(reader.GetString(0));
									var quote_item = new Quote()
									{
										Ask = quote.AskPrice,
										Bid = quote.BidPrice
									};
									break;

								case TradeHistoryType.TimeAndSale:
									var ts = JsonConvert.DeserializeObject<TradeHistoryTimeAndSale>(reader.GetString(0));
									var ts_item = new Tick()
									{
										Ask = ts.AskPrice,
										Bid = ts.BidPrice,
										Price = ts.Price
									};
									break;
							}
						}
					}
				}

				connection.Close();
			}
		}
	}
}
