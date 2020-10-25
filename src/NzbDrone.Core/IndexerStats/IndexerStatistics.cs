using NzbDrone.Core.Datastore;

namespace NzbDrone.Core.HistoryStats
{
    public class IndexerStatistics : ResultSet
    {
        public int TotalSearches { get; set; }
    }
}
