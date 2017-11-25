using Armyknife.Business;
using Microsoft.Extensions.DependencyInjection;
using Armyknife.Business.Interfaces;
using System;
using System.Threading.Tasks;

namespace Armyknife
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var serviceCollection = new ServiceCollection();
         DependencyRegistration.RegisterDependencies(serviceCollection);
         var provider = serviceCollection.BuildServiceProvider();

         var executor = provider.GetService<IExecutor>();
         int exitCode = await executor.ExecuteAsync(args);
         Environment.Exit(exitCode);
      }
   }
}
