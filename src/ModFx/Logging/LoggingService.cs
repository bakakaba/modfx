using Autofac;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace ModFx.Logging
{
    public class LoggingService
    {
        private const string outputTemplate =
            "[{Timestamp:HH:mm:ss} {Level:u3} ({MachineName}>{ProcessId}>{ThreadId})]{SourceContext}{NewLine}{Message}{NewLine}{Exception}";

        public static void Configure(ContainerBuilder builder, LoggingConfiguration configuration)
        {
            builder.RegisterInstance(configuration);

            var levelSwitch = new LoggingLevelSwitch();
            builder.RegisterInstance(levelSwitch);

            Log.Logger = new Serilog.LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .WriteTo.LiterateConsole(outputTemplate: outputTemplate)
                .CreateLogger();

            levelSwitch.MinimumLevel = configuration.Level;

            RegisterMicrosoftLoggingFramework(builder);
        }

        private static void RegisterMicrosoftLoggingFramework(ContainerBuilder builder)
        {
            builder.RegisterSource(new LoggerRegistrationSource());

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger(nameof(ModFx));
            loggerFactory.AddSerilog();

            builder
                .RegisterInstance(loggerFactory)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}