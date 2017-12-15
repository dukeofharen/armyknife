using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Armyknife.Business.Interfaces;
using System.Linq;

namespace Armyknife.Business.Tests
{
    [TestClass]
    public class DependencyRegistrationFacts
    {
        private ServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void Initialize()
        {
            _serviceCollection = new ServiceCollection();
            DependencyRegistration.RegisterDependencies(_serviceCollection);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void DependencyRegistration_ResolveExecutor_HappyFlow()
        {
            // act
            var executor = _serviceProvider.GetService<IExecutor>();

            // assert
            Assert.IsNotNull(executor);
        }

        [TestMethod]
        public void DependencyRegistration_ResolveAllDependencies_HappyFlow()
        {
            // act / assert
            var services = _serviceCollection.Where(s => s.ServiceType != typeof(ITool));
            foreach(var service in _serviceCollection)
            {
                var instance = _serviceProvider.GetService(service.ServiceType);
                Assert.IsNotNull(instance);
            }
        }

        [TestMethod]
        public void DependencyRegistration_ResolveTools_HappyFlow()
        {
            // act
            var services = _serviceProvider.GetServices<ITool>();

            // assert
            Assert.IsTrue(services.Any());
        }
    }
}
