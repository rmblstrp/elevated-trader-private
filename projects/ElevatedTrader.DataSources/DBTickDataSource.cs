using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Impl;
using NHibernate.Loader.Criteria;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace ElevatedTrader.DataSources
{
	public class DBTickDataSource : ITickDataSource
	{
		private enum DatabaseType
		{
			Unknown,
			SqlServer,
			Postgres,
			MySql
		}

		private class ConfigurationSettings
		{
			public string ConnectionString { get; set; }

			public DatabaseType DatabaseType { get; set; }
		}

		private class HistoryEntry
		{
			public virtual long Id { get; set; }

			public virtual string Symbol { get; set; }

			public virtual DateTime Recorded { get; set; }

			public virtual TradeHistoryType Type { get; set; }

			public virtual string Json { get; set; }

			public class Mapping : ClassMap<HistoryEntry>
			{
				public Mapping()
				{
					Id(x => x.Id);
					Map(x => x.Symbol);
					Map(x => x.Recorded);
					Map(x => x.Type).CustomType<int>();
					Map(x => x.Json);
				}
			}
		}

		const int InitialCapacity = 100000000;

		private List<TickDelta> deltas = new List<TickDelta>(InitialCapacity);
		private List<ITick> ticks = new List<ITick>(InitialCapacity);
		private ISessionFactory factory;
		private long lastId = 0;

		public IList<TickDelta> Deltas
		{
			get { return deltas; }
		}

		public IList<ITick> Ticks
		{
			get { return ticks; }
		}

		public DBTickDataSource()
		{
		}

		public void Clear()
		{
			deltas.Clear();
			ticks.Clear();
		}

		public void Configure(string json)
		{
			var settings = JsonConvert.DeserializeObject<ConfigurationSettings>(json);

			factory = Fluently.Configure()
				.Database(ConfigureDatabase(settings.DatabaseType, settings.ConnectionString))
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

		public void Load(string symbol, int? count = null)
		{
			using (var session = factory.OpenStatelessSession())
			{
				var criteria = session.CreateCriteria<HistoryEntry>()
					.Add(Expression.Gt("id", lastId))
					.Add(Expression.Eq("symbol", symbol))
					.Add(Expression.Eq("type", (int)TradeHistoryType.TimeAndSale))
					.AddOrder(Order.Asc("id"));
					;

				if (count.HasValue)
				{
					criteria.SetMaxResults(count.Value);
				}

				using (var command = session.Connection.CreateCommand())
				{
					command.CommandType = CommandType.Text;
					command.CommandText = GetGeneratedSql(criteria);
					command.Parameters.Add(lastId);
					command.Parameters.Add(symbol);
					command.Parameters.Add((int)TradeHistoryType.TimeAndSale);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var ts = JsonConvert.DeserializeObject<TradeHistoryTimeAndSale>(reader.GetString(4));
							lastId = reader.GetInt64(0);

							var tick = new Tick()
							{
								Time = ts.Time,
								Price = ts.Price,
								Bid = ts.BidPrice,
								Ask = ts.AskPrice
							};

							ticks.Add(tick);

							if (Ticks.Count > 1)
							{
								deltas.Add(tick - ticks[ticks.Count - 2]);
							}
						}

						reader.Close();
					}
				}

				session.Close();
			}
		}

		private string GetGeneratedSql(ICriteria criteria)
		{
			var criteriaImpl = (CriteriaImpl)criteria;
			var sessionImpl = (SessionImpl)criteriaImpl.Session;
			var factory = (SessionFactoryImpl)sessionImpl.SessionFactory;
			var implementors = factory.GetImplementors(criteriaImpl.EntityOrClassName);
			var loader = new CriteriaLoader((IOuterJoinLoadable)factory.GetEntityPersister(implementors[0]), factory, criteriaImpl, implementors[0], sessionImpl.EnabledFilters);

			return loader.SqlString.ToString();
		}
	}
}
