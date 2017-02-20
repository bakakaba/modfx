using Autofac;

namespace ModFx.Configuration
{
    public class ConfigurationService
    {
        public static IConfigurationFactory Configure(ContainerBuilder builder)
        {
            var fac = new ConfigurationFactory();
            builder
                .RegisterInstance(fac)
                .AsSelf()
                .AsImplementedInterfaces();

            return fac;
        }
    }
}