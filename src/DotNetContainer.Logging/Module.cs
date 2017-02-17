using Autofac;
using DotNetContainer.Core;
using DotNetContainer.Logging.RegistrationSources;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace DotNetContainer.Logging
{
    public class Module : BaseModule<Configuration>
    {
        protected override void Register(ContainerBuilder builder)
        {
            var levelSwitch = new LoggingLevelSwitch();
            builder.RegisterInstance(levelSwitch);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .CreateLogger();

            levelSwitch.MinimumLevel = Configuration.Level;

            RegisterMicrosoftLoggingFramework(builder);
        }

        private void RegisterMicrosoftLoggingFramework(ContainerBuilder builder)
        {
            builder.RegisterSource(new LoggerSource());

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog();

            builder.RegisterInstance(loggerFactory);
        }
    }
}