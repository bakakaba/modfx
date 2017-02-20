using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ModFx.Configuration;
using ModFx.Extensions;
using ModFx.Logging;
using ModFx.Models;
using Serilog;

namespace ModFx
{
    public class Framework
    {
        public static Framework Instance { get; private set; }
        public IConfigurationFactory ConfigurationFactory { get; }

        private readonly IReadOnlyCollection<AssemblyExtended> _assemblies;
        private readonly IContainer _container;

        protected Framework()
        {
            var cb = new ContainerBuilder();

            ConfigurationFactory = ConfigurationService.Configure(cb);
            LoggingService.Configure(cb, ConfigurationFactory.Get<Logging.LoggingConfiguration>());
            Log.Verbose("Configuration and logging loaded");

            _assemblies = Assembly
                .GetEntryAssembly()
                .LoadAssemblies();
            Log.Verbose("{Count} ({ModuleCount} modules) assemblies loaded \n{Assemblies}",
                _assemblies.Count,
                _assemblies.Where(x => x.IsModule).Count(),
                string.Join(string.Empty, _assemblies.Select(x => x.ToString())));

            var modules = _assemblies
                .Where(x => x.IsModule)
                .Select(x => x.Assembly)
                .ToArray();
            cb.RegisterAssemblyModules(modules);

            Log.Verbose("Building container");
            _container = cb.Build();
        }

        /// <summary>
        /// Service locator function. Should only be used to get the entrypoint service.
        /// </summary>
        /// <returns>Service that is requested from the container.</returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Initializes the framework for use. This should only be run
        /// once per application.
        /// </summary>
        /// <returns>Framework object to directly access the framework functionality.</returns>
        public static Framework Initialize()
        {
            if (Instance != null)
                throw new InvalidOperationException(
                    "The framework has already been initialized. This should only be run once per application.");

            Instance = new Framework();

            return Instance;
        }
    }
}