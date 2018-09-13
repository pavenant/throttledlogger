namespace CachedLoggerHost
{
    public interface IConfigManager
    {
        int TimerInterval { get; set; }
        bool UseLog4Net { get; set; }
        bool Refresh();
    }
}