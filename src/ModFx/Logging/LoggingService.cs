using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Logging;
using ModFx.Configuration;
using Serilog;
using Serilog.Core;

namespace ModFx.Logging
{
    public class LoggingService
    {
        public static void Configure(
            ContainerBuilder builder,
            IConfigurationFactory configurationFactory,
            IEnumerable<Assembly> assemblies)
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

            ApplyExtensions(configurationFactory, loggerCfg, assemblies);

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

        private static void ApplyExtensions(
            IConfigurationFactory configurationFactory,
            Serilog.LoggerConfiguration loggerConfiguration,
            IEnumerable<Assembly> assemblies)
        {
            var exts = assemblies
                .SelectMany(x => x.DefinedTypes)
                .Where(x => x.IsSubclassOf(typeof(ILoggingExtension)));

            foreach (var ext in exts)
            {
                 var extObj = (ILoggingExtension)Activator.CreateInstance(ext.AsType());
                 extObj.Configure(loggerConfiguration, configurationFactory);
            }
        }
    }
}