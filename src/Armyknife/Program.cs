using Armyknife.Business;
using Microsoft.Extensions.DependencyInjection;
using Armyknife.Business.Interfaces;
using System;
using System.Threading.Tasks;
using Armyknife.DI.DnCore;

namespace Armyknife
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var serviceCollection = new ServiceCollection();
         var wrapper = new DnCoreServiceContainerWrapper(serviceCollection);
         DependencyRegistration.RegisterDependencies(wrapper);
         var provider = serviceCollection.BuildServiceProvider();
         wrapper.Provider = provider;

         var executor = provider.GetService<IExecutor>();
         int exitCode = await executor.ExecuteAsync(args);
         Environment.Exit(exitCode);
      }
   }
}
