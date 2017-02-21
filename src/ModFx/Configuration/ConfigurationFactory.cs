using System;
using ModFx.Extensions;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ModFx.Configuration
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

        public T Get<T>(params Type[] inheritedModuleTypes)
        {
            var type = typeof(T);
            var moduleName = type.Namespace;

            var inheritedModuleNames = inheritedModuleTypes
                .Select(x => x.Namespace)
                .ToArray();

            return Get<T>(moduleName, inheritedModuleNames);
        }

        public T Get<T>(string moduleName, params string[] inheritedModuleNames)
        {
            var moduleSection = _configurationRoot.GetSection(moduleName);

            var globalSection = _configurationRoot.GetSection(GlobalSectionName);
            moduleSection = moduleSection.Merge(globalSection);

            foreach(var inheritedModuleName in inheritedModuleNames)
            {
                var inheritedSection = _configurationRoot.GetSection(inheritedModuleName);
                moduleSection = moduleSection.Merge(inheritedSection);
            }

            return moduleSection.Get<T>();
        }
    }
}