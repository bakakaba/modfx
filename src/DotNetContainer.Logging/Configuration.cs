using Serilog.Events;

namespace DotNetContainer.Logging
{
    public class Configuration
    {
        public LogEventLevel Level { get; set; } = LogEventLevel.Information;
    }
}