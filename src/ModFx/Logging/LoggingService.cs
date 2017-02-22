using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Logging;
using ModFx.Configuration;
using ModFx.Extensions;
using Serilog;
using Serilog.Core;

namespace ModFx.Logging
{
    public class LoggingService
    {
        public static void Configure(
            ContainerBuilder builder,
            IConfigurationFactory configurationFactory,
            IEnumerable<Action<LoggerConfiguration, IConfigurationFactory>> loggingExtensions)
        {
            var cfg = configurationFactory.Get<LoggingConfiguration>();
            builder.RegisterInstance(cfg);

            var levelSwitch = new LoggingLevelSwitch();
            builder.RegisterInstance(levelSwitch);

            var loggerCfg = new Serilog.LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .WriteTo.LiterateConsole(outputTemplate: cfg.Template);

            loggingExtensions.ForEach(x => x(loggerCfg, configurationFactory));

            Log.Logger = loggerCfg
                .CreateLogger();

            levelSwitch.MinimumLevel = cfg.Level;

            RegisterMicrosoftLoggingFramework(builder);
        }

        private static void RegisterMicrosoftLoggingFramework(ContainerBuilder builder)
        {
            builder.RegisterSource(new LoggerRegistrationSource());

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog();

            builder
                .RegisterInstance(loggerFactory)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}