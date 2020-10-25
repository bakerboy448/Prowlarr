using System.Collections.Generic;
using System.Linq;
using NzbDrone.Common.Cache;
using NzbDrone.Core.Messaging;
using NzbDrone.Core.Messaging.Events;

namespace NzbDrone.Core.HistoryStats
{
    public interface IHistoryStatisticsService
    {
        List<HistoryStatistics> HistoryStatistics();
    }

    public class HistoryStatisticsService : IHistoryStatisticsService
    {
        private readonly IHistoryStatisticsRepository _historyStatisticsRepository;
        private readonly ICached<List<IndexerStatistics>> _cache;

        public HistoryStatisticsService(IHistoryStatisticsRepository historyStatisticsRepository,
                                       ICacheManager cacheManager)
        {
            _historyStatisticsRepository = historyStatisticsRepository;
            _cache = cacheManager.GetCache<List<IndexerStatistics>>(GetType());
        }

        public List<HistoryStatistics> HistoryStatistics()
        {
            var indexerStatistics = _cache.Get("AllHistory", () => _historyStatisticsRepository.HistoryStatistics());

            return indexerStatistics.GroupBy(s => s.HistoryId).Select(s => MapHistoryStatistics(s.ToList())).ToList();
        }

        private HistoryStatistics MapHistoryStatistics(List<IndexerStatistics> indexerStatistics)
        {
            var historyStatistics = new HistoryStatistics
            {
                TotalSearches = indexerStatistics.Sum(s => s.TotalSearches)
            };

            return historyStatistics;
        }
    }
}
