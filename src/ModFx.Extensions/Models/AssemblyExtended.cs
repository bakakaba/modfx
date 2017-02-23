using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ModFx.Extensions.Models
{
    public class AssemblyExtended
    {
        public AssemblyExtended(Assembly assembly, IReadOnlyCollection<AssemblyName> directDependencies)
        {
            Assembly = assembly;
            DirectDependencies = directDependencies;
        }

        public Assembly Assembly { get; }
        public IReadOnlyCollection<AssemblyName> DirectDependencies { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Assembly.FullName}");

            foreach(var dep in DirectDependencies)
                sb.AppendLine($"\t->{dep.FullName}");

            return sb.ToString();
        }
    }
}