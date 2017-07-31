using System.Reflection;

namespace Armyknife.Services.Implementations
{
    public class AssemblyService : IAssemblyService
    {
        public string GetVersionNumber()
        {
            var assembly = Assembly.GetEntryAssembly();
            return assembly.GetName().Version.ToString();
        }
    }
}
