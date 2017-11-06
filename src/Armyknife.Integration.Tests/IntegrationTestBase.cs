using Armyknife.Business;
using Armyknife.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Armyknife.IntegrationTests
{
   public abstract class IntegrationTestBase
   {
      protected Mock<IOutputWriter> _outputWriterMock;
      protected string _output;
      protected IExecutor _executor;

      [TestInitialize]
      public void Initialize()
      {
         var serviceCollection = new ServiceCollection();
         DependencyRegistration.RegisterDependencies(serviceCollection);

         _outputWriterMock = new Mock<IOutputWriter>();
         _outputWriterMock
            .Setup(m => m.WriteOutput(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
            .Callback<string, IDictionary<string, string>>((result, a) => _output = result);

         serviceCollection.AddSingleton(_outputWriterMock.Object);

         var provider = serviceCollection.BuildServiceProvider();

         _executor = provider.GetService<IExecutor>();
      }

      [TestCleanup]
      public void Cleanup()
      {
         _outputWriterMock.VerifyAll();
      }

      protected string[] GetArgs(string input)
      {
         return input.Split(' ');
      }
   }
}
