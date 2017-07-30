using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Business.Tests
{
    [TestClass]
    public class DependencyRegistrationFacts
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void Initialize()
        {
            var serviceCollection = new ServiceCollection();
            DependencyRegistration.RegisterDependencies(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void DependencyRegistration_ResolveExecutor_HappyFlow()
        {
            // act
            var executor = _serviceProvider.GetService<IExecutor>();

            // assert
            Assert.IsNotNull(executor);
        }
    }
}
