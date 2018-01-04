using Armyknife.Business;
using Armyknife.Business.Interfaces;
using System;
using System.Threading.Tasks;
using Armyknife.DI.Unity;

namespace Armyknife
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var wrapper = UnityServiceContainerWrapper.GetInstance();
         DependencyRegistration.RegisterDependencies(wrapper);

         var executor = wrapper.Resolve<IExecutor>();
         int exitCode = await executor.ExecuteAsync(args);
         Environment.Exit(exitCode);
      }
   }
}
