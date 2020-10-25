using System.Text.Json;

namespace Prowlarr.Api.V1.Statistics
{
  public class StatisticsModule : ProwlarrV1Module
  {
    public StatisticsModule()
      : base("statistics")
    {
        Get("/", x => JsonSerializer.Serialize(GetStatistics()));
    }

    public ProwlarrStats GetStatistics()
    {
        var prowlarrStats = new ProwlarrStats
        {
            Searches = 100
        };

        return prowlarrStats;
    }
  }
}
