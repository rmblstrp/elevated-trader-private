using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Impl;
using NHibernate.Loader.Criteria;
using NHibernate.Persister.Entity;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace ElevatedTrader.DataSources
{
	public class DBTickDataSource : ITickDataSource
	{
		public enum DatabaseType
		{
			Unknown,
			SqlServer,
			Postgres,
			MySql
		}

		private class HistoryEntry
		{
			public virtual int Id { get; set; }

			public virtual string Symbol { get; set; }

			public virtual DateTime Recorded { get; set; }

			public virtual TradeHistoryType Type { get; set; }

			public virtual string Json { get; set; }

			public class Mapping : ClassMap<HistoryEntry>
			{
				public Mapping()
				{
					Table("SymbolHistory");
					Id(x => x.Id);
					Map(x => x.Symbol);
					//Map(x => x.Recorded);
					Map(x => x.Type).CustomType<int>();
					Map(x => x.Json);
				}
			}
		}

		const int InitialCapacity = 100000000;

		private List<ITick> ticks = new List<ITick>(InitialCapacity);
		private ISessionFactory factory;
		private int lastId = 0;

		public IList<ITick> Ticks
		{
			get { return ticks; }
		}

		public DBTickDataSource()
		{
		}

		public void Clear()
		{
			lastId = 0;
			ticks.Clear();
		}

		public void Configure(dynamic configuration)
		{
			var db = ConfigureDatabase((DatabaseType)Enum.Parse(typeof(DatabaseType), configuration.DatabaseType), (string)configuration.ConnectionString);

			factory = Fluently.Configure()
				.Database(db)
				.Mappings(m => m.FluentMappings.Add<HistoryEntry.Mapping>())
				.BuildSessionFactory()
				;
		}

		private IPersistenceConfigurer ConfigureDatabase(DatabaseType type, string connectionString)
		{
			switch (type)
			{
				case DatabaseType.MySql:
					return MySQLConfiguration.Standard.ConnectionString(connectionString);
				case DatabaseType.Postgres:
					return PostgreSQLConfiguration.PostgreSQL82.ConnectionString(connectionString);
				case DatabaseType.SqlServer:
					return MsSqlConfiguration.MsSql2008.ConnectionString(connectionString);
			}

			throw new NotSupportedException();
		}

		public void Load(string symbol, int? count = null, Func<ITick, bool> added = null)
		{
			try
			{
				InternalLoad(symbol, count, added);
			}
			catch (Exception ex)
			{
				while (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}

				throw ex;
			}
		}

		private void InternalLoad(string symbol, int? count = null, Func<ITick, bool> added = null)
		{
			using (var session = this.factory.OpenSession())
			{
				var criteria = session.CreateCriteria<HistoryEntry>()
					.Add(Expression.Gt("Id", lastId))
					.Add(Expression.Eq("Symbol", symbol))
					.Add(Expression.Eq("Type", (int)TradeHistoryType.TimeAndSale))
					.AddOrder(Order.Asc("Id"))
					;

				if (count.HasValue)
				{
					criteria.SetMaxResults(count.Value);
				}

				var criteriaImpl = (CriteriaImpl)criteria;
				var sessionImpl = (SessionImpl)criteriaImpl.Session;
				var factory = (SessionFactoryImpl)sessionImpl.SessionFactory;
				var implementors = factory.GetImplementors(criteriaImpl.EntityOrClassName);
				var loader = new CriteriaLoader((IOuterJoinLoadable)factory.GetEntityPersister(implementors[0]), factory, criteriaImpl, implementors[0], sessionImpl.EnabledFilters);

				var sql_command = loader.CreateSqlCommand(loader.Translator.GetQueryParameters(), sessionImpl);
				var sql = sql_command.Query;

				var command = factory.ConnectionProvider.Driver.GenerateCommand(CommandType.Text, sql, sql_command.ParameterTypes);
				command.Connection = factory.ConnectionProvider.GetConnection();
				sql_command.Bind(command, sessionImpl);
				command.CommandTimeout = 60;

				using (command)
				{
					using (var reader = command.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								//var ts = JsonConvert.DeserializeObject<TradeHistoryTimeAndSale>(reader.GetString(3));
								var ts = new TradeHistoryTimeAndSale();
								lastId = reader.GetInt32(0);

								var tick = new Tick()
								{
									Time = ts.Time,
									Price = ts.Price,
									Bid = ts.BidPrice,
									Ask = ts.AskPrice
								};

								ticks.Add(tick);

								if (added != null && !added(tick))
								{
									break;
								}
							}
						}
						catch (Exception ex)
						{
							throw ex;
						}

						reader.Close();
					}
				}

				session.Close();
			}
		}
	}
}
