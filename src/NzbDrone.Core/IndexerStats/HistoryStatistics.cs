using System.Collections.Generic;
using NzbDrone.Core.Datastore;

namespace NzbDrone.Core.HistoryStats
{
    public class HistoryStatistics : ResultSet
    {
        public int TotalSearches { get; set; }
    }
}
