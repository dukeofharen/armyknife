using Armyknife.Business.Implementations;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Armyknife.Business
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IExecutor, Executor>();
            serviceCollection.AddTransient<IInputReader, InputReader>();
            serviceCollection.AddTransient<IOutputWriter, OutputWriter>();
            serviceCollection.AddTransient<IToolResolver, ToolResolver>();

            var toolTypes = AssemblyHelper.GetImplementations<ITool>();
            foreach (var type in toolTypes)
            {
                serviceCollection.AddTransient(typeof(ITool), type);
            }

            Services.DependencyRegistration.RegisterDependencies(serviceCollection);
        }
    }
}
