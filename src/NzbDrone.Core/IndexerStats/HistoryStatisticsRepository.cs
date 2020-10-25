using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NzbDrone.Core.Datastore;
using NzbDrone.Core.History;

namespace NzbDrone.Core.HistoryStats
{
    public interface IHistoryStatisticsRepository
    {
        List<IndexerStatistics> HistoryStatistics();
    }

    public class HistoryStatisticsRepository : IHistoryStatisticsRepository
    {
        private const string _selectTemplate = "SELECT /**select**/ FROM History";

        private readonly IMainDatabase _database;

        public HistoryStatisticsRepository(IMainDatabase database)
        {
            _database = database;
        }

        public List<IndexerStatistics> HistoryStatistics()
        {
            var time = DateTime.UtcNow;
            return Query(Builder().Where<History>(x => x.date < time));
        }

        private List<IndexerStatistics> Query(SqlBuilder builder)
        {
            var sql = builder.AddTemplate(_selectTemplate).LogQuery();

            using (var conn = _database.OpenConnection())
            {
                return conn.Query<IndexerStatistics>(sql.RawSql, sql.Parameters).ToList();
            }
        }

        private SqlBuilder Builder() => new SqlBuilder()
            .Select(@"COUNT(History.Id) AS TotalSearches");
    }
}
