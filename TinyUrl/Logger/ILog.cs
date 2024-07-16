namespace TinyUrl.Logger
{
    public interface ILog
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
