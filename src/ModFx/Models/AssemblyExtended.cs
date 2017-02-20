using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ModFx.Extensions;

namespace ModFx.Models
{
    public class AssemblyExtended
    {
        public AssemblyExtended(Assembly assembly, IReadOnlyCollection<AssemblyName> directDependencies)
        {
            Assembly = assembly;
            DirectDependencies = directDependencies;
            IsModule = assembly.IsModule();
        }

        public Assembly Assembly { get; }
        public IReadOnlyCollection<AssemblyName> DirectDependencies { get; }
        public bool IsModule { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Assembly.FullName} (IsModule: {IsModule})");

            foreach(var dep in DirectDependencies)
                sb.AppendLine($"\t->{dep.FullName}");

            return sb.ToString();
        }
    }
}