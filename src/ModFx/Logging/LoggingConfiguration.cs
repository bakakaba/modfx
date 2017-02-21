using Serilog.Events;

namespace ModFx.Logging
{
    public class LoggingConfiguration
    {
        public LogEventLevel Level { get; set; } = LogEventLevel.Information;
        public string Template { get; set; } =
            "[{Timestamp:HH:mm:ss} {Level:u3} pid:{ProcessId} tid:{ThreadId}]"
            +" {Message}{NewLine}{Exception}";
    }
}