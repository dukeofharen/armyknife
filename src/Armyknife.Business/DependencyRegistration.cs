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

            // This is done so the types in the Armyknife.Tools project are preloaded and available through reflection.
           // ReSharper disable once UnusedVariable
            string toolName = typeof(Armyknife.Tools.Implementations.Base64DecodeTool).ToString();

            var toolTypes = AssemblyHelper.GetImplementations<ITool>();
            foreach (var type in toolTypes)
            {
                serviceCollection.AddTransient(typeof(ITool), type);
            }

            Services.DependencyRegistration.RegisterDependencies(serviceCollection);
        }
    }
}
