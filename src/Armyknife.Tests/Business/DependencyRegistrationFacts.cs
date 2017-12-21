using System.Linq;
using Armyknife.Business;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Business
{
   [TestClass]
   public class DependencyRegistrationFacts
   {
      private IServiceCollection _serviceCollection;
      private DnCoreServiceContainerWrapper _wrapper;

      [TestInitialize]
      public void Initialize()
      {
         _serviceCollection = new ServiceCollection();
         _wrapper = new DnCoreServiceContainerWrapper(_serviceCollection);
         DependencyRegistration.RegisterDependencies(_wrapper);
         _wrapper.Provider = _serviceCollection.BuildServiceProvider();
      }

      [TestMethod]
      public void DependencyRegistration_ResolveExecutor_HappyFlow()
      {
         // act
         var executor = _wrapper.Resolve<IExecutor>();

         // assert
         Assert.IsNotNull(executor);
      }

      [TestMethod]
      public void DependencyRegistration_ResolveAllDependencies_HappyFlow()
      {
         // act / assert
         var services = _serviceCollection.Where(s => s.ServiceType != typeof(ITool));
         foreach (var service in services)
         {
            var instance = _wrapper.Resolve(service.ServiceType);
            Assert.IsNotNull(instance);
         }
      }

      [TestMethod]
      public void DependencyRegistration_ResolveTools_HappyFlow()
      {
         // act
         var services = _wrapper.ResolveMultiple<ITool>();

         // assert
         Assert.IsTrue(services.Any());
      }
   }
}
