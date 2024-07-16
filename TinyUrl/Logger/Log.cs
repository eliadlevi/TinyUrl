using SerlogILogger = Serilog.ILogger;

namespace TinyUrl.Logger
{
    public class Log : ILog
    {
        private readonly SerlogILogger _logger;
        public Log(SerlogILogger logger)
        {
            _logger = logger;

            //_logger  = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public void LogInfo(string message)
        {
            _logger.Information(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warning(message);
        }
        public void LogError(string message)
        {
            _logger.Error(message);
        }
    }
}
