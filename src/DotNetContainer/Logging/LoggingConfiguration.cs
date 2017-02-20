using Serilog.Events;

namespace DotNetContainer.Logging
{
    public class LoggingConfiguration
    {
        public LogEventLevel Level { get; set; } = LogEventLevel.Verbose;
    }
}