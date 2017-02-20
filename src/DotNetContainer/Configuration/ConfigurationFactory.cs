using System;
using DotNetContainer.Extensions;
using Microsoft.Extensions.Configuration;

namespace DotNetContainer.Configuration
{
    public class ConfigurationFactory : IConfigurationFactory
    {
        private const string EnvironmentNameKey = "EnvironmentName";
        private const string GlobalSectionName = "global";
        private IConfigurationRoot _configurationRoot;

        public ConfigurationFactory()
        {
            var environmentName = Environment.GetEnvironmentVariable("EnvironmentNameKey");
            _configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{environmentName}.json", optional: true)
                .Build();
        }

        public T Get<T>()
        {
            var type = typeof(T);
            var moduleName = type.Namespace;

            return Get<T>(moduleName);
        }

        public T Get<T>(string moduleName)
        {
            return GetSection(moduleName).Get<T>();
        }

        private IConfigurationSection GetSection(string sectionName)
        {
            var globalSection = _configurationRoot.GetSection(GlobalSectionName);
            var moduleSection = _configurationRoot.GetSection(sectionName);

            foreach (var item in globalSection.AsEnumerable())
            {
                if (moduleSection[item.Key].IsNullOrWhiteSpace())
                    moduleSection[item.Key] = item.Value;
            }

            return moduleSection;
        }
    }
}