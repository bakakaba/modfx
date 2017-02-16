using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using DotNetContainer.RegistrationSources;

namespace DotNetContainer
{
    public class Framework
    {
        private static Framework _instance;
        private readonly IContainer _container;

        protected Framework()
        {
            var assemblies = LoadAssemblies();

            var cb = new ContainerBuilder();
            cb.RegisterSource(new LoggerSource());
            cb.RegisterAssemblyModules(assemblies);
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
            if (_instance != null)
                throw new InvalidOperationException(
                    "The framework has already been initialized. This should only be run once per application.");

            _instance = new Framework();

            return _instance;
        }

        private static Assembly[] LoadAssemblies()
        {
            var assemblies = new List<Assembly>();
            var stack = new Stack<Assembly>();

            var entryAsm = Assembly.GetEntryAssembly();
            assemblies.Add(entryAsm);
            stack.Push(entryAsm);

            do
            {
                foreach (var reference in stack.Pop().GetReferencedAssemblies())
                {
                    var name = reference.Name;
                    if (assemblies.Any(x => x.FullName.StartsWith(name)))
                        continue;

                    var assembly = Assembly.Load(reference);
                    assemblies.Add(assembly);
                    stack.Push(assembly);
                }
            }
            while (stack.Count > 0);

            return assemblies.ToArray();
        }
    }
}