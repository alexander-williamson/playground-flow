namespace Flow.Library.Configuration
{
    public interface IConfiguration
    {
        bool CollectCastleWindsorPerformanceCounters { get; }
        string ConnectionString { get; }
    }
}
