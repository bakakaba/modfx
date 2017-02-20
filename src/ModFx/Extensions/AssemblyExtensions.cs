using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModFx.Models;

namespace ModFx.Extensions
{
    public static class AssemblyExtensions
    {
        public static IReadOnlyCollection<AssemblyExtended> LoadAssemblies(this Assembly entryAssembly)
        {
            var assemblies = new List<AssemblyExtended>();
            var stack = new Stack<Assembly>();

            stack.Push(entryAssembly);

            do
            {
                var assembly = stack.Pop();
                var directDependencies = assembly.GetReferencedAssemblies();
                assemblies.Add(new AssemblyExtended(assembly, directDependencies));
                foreach (var reference in assembly.GetReferencedAssemblies())
                {
                    if (assemblies.Any(x => x.Assembly.FullName.StartsWith(reference.Name)))
                        continue;

                    var referencedAssembly = Assembly.Load(reference);
                    stack.Push(referencedAssembly);
                }
            }
            while (stack.Count > 0);

            return assemblies
                .OrderByDescending(x => x.IsModule)
                .ThenBy(x => x.Assembly.FullName)
                .ToList();
        }

        public static bool IsModule(this Assembly assembly)
        {
            return assembly.DefinedTypes.Any(x => x.IsSubclassOf(typeof(BaseModule)));
        }
    }
}
