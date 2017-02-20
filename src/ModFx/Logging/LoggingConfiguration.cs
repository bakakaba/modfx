using Serilog.Events;

namespace ModFx.Logging
{
    public class LoggingConfiguration
    {
        public LogEventLevel Level { get; set; } = LogEventLevel.Verbose;
    }
}