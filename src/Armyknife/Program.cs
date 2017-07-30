using Armyknife.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Armyknife
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            DependencyRegistration.RegisterDependencies(serviceCollection);
            var provider = serviceCollection.BuildServiceProvider();

            var executor = provider.GetService<IExecutor>();
            executor.Execute(args);
        }
    }
}