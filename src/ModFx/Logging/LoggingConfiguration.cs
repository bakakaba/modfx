using Serilog.Events;

namespace ModFx.Logging
{
    public class LoggingConfiguration
    {
        public LogEventLevel Level { get; set; } = LogEventLevel.Information;
        public string Template { get; set; } =
            "[{Timestamp:HH:mm:ss} pid:{ProcessId} tid:{ThreadId} {Level:u3}]"
            +" {Message}{NewLine}{Exception}";
    }
}