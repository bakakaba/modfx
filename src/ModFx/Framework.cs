using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using ModFx.Configuration;
using ModFx.Extensions;
using ModFx.Extensions.Models;
using ModFx.Logging;
using Serilog;

namespace ModFx
{
    public class Framework : IDisposable
    {
        public static Framework Instance { get; private set; }
        public IConfigurationFactory Configuration { get; }

        private readonly IReadOnlyCollection<AssemblyExtended> _assemblies;
        private readonly IContainer _container;

        protected Framework(IEnumerable<Action<LoggerConfiguration, IConfigurationFactory>> loggingExtensions)
        {
            var sw = Stopwatch.StartNew();
            var cb = new ContainerBuilder();

            Configuration = ConfigurationFactory.Configure(cb);
            LoggingService.Configure(cb, Configuration, loggingExtensions);

            _assemblies = Assembly.GetEntryAssembly().LoadAssemblies();
            Log.Verbose("{Count} assemblies loaded \n{Assemblies}",
                _assemblies.Count,
                string.Join(string.Empty, _assemblies.Select(x => x.ToString())));

            var modules = _assemblies
                .Select(x => x.Assembly)
                .Where(x => x.DefinedTypes.Any(t => t.IsSubclassOf(typeof(BaseModule))))
                .ToArray();
            cb.RegisterAssemblyModules(modules);

            _container = cb.Build();
            Log.Information(
                "Framework loaded {ModuleCount} modules from {AssemblyCount} assemblies in {Elapsed}",
                modules.Length, _assemblies.Count, sw.Elapsed);
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
        public static Framework Initialize(params Action<LoggerConfiguration, IConfigurationFactory>[] loggingExtensions)
        {
            if (Instance != null)
                throw new InvalidOperationException(
                    "The framework has already been initialized. This should only be run once per application.");

            Instance = new Framework(loggingExtensions);

            return Instance;
        }

        public void Dispose()
        {
            _container.Dispose();
            Instance = null;

            Log.Information("Framework disposed");
            Log.CloseAndFlush();
        }
    }
}