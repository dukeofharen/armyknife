using Armyknife.Business;
using Microsoft.Extensions.DependencyInjection;
using Armyknife.Business.Interfaces;
using System;

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
         int exitCode = executor.ExecuteAsync(args).Result;
         Environment.Exit(exitCode);
      }
   }
}
