using ModFx.Configuration;
using Serilog;
using Serilog.Formatting.Compact;

namespace ModFx.Logging.File
{
    public class FileLogging : ILoggingExtension
    {
        public void Configure(
            LoggerConfiguration loggerConfiguration,
            IConfigurationFactory configurationFactory)
        {
            var cfg = configurationFactory
                .Get<FileLoggingConfiguration>(typeof(LoggingConfiguration));

            loggerConfiguration.WriteTo.RollingFile(
                new RenderedCompactJsonFormatter(),
                cfg.PathFormat,
                buffered: cfg.Buffered,
                fileSizeLimitBytes: cfg.MaxFileSize,
                retainedFileCountLimit: cfg.MaxFileCount);
        }
    }
}