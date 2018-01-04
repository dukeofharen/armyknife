using System.Linq;
using Armyknife.Business;
using Armyknife.Business.Interfaces;
using Armyknife.DI.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Business
{
   [TestClass]
   public class DependencyRegistrationFacts
   {
      private UnityServiceContainerWrapper _wrapper;

      [TestInitialize]
      public void Initialize()
      {
         _wrapper = new UnityServiceContainerWrapper();
         DependencyRegistration.RegisterDependencies(_wrapper);
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
         var serviceTypes = _wrapper.GetInterfaceTypes().Where(t => t != typeof(ITool));
         foreach (var serviceType in serviceTypes)
         {
            var instance = _wrapper.Resolve(serviceType);
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
