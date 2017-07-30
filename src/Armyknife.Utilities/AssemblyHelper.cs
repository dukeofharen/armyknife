using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Armyknife.Utilities
{
    public static class AssemblyHelper
    {
        public static IEnumerable<Type> GetImplementations<TInterface>()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblyNames = entryAssembly.GetReferencedAssemblies();

            var result = assemblyNames
                .Select(Assembly.Load)
                .SelectMany(a => a.DefinedTypes)
                .Where(a => a.ImplementedInterfaces.Contains(typeof(TInterface)))
                .Select(ti => ti.AsType());

            return result;
        }
    }
}
