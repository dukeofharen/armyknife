using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Armyknife.Business.Interfaces;

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
            foreach(var service in _serviceCollection)
            {
                var instance = _serviceProvider.GetService(service.ServiceType);
                Assert.IsNotNull(instance);
            }
        }
    }
}
