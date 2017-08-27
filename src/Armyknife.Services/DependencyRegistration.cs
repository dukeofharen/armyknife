using Armyknife.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Armyknife.Services
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAssemblyService, AssemblyService>();
            serviceCollection.AddTransient<IConsoleService, ConsoleService>();
            serviceCollection.AddTransient<IFileService, FileService>();
            serviceCollection.AddTransient<IMimeService, MimeService>();
        }
    }
}
