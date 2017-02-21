using ModFx.Configuration;
using Serilog;

namespace ModFx.Logging
{
    public interface ILoggingExtension
    {
        void Configure(
            LoggerConfiguration loggerConfiguration,
            IConfigurationFactory configurationFactory);
    }
}