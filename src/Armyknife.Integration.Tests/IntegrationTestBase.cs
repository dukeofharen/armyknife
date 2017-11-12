using Armyknife.Business;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;

namespace Armyknife.Integration.Tests
{
   public abstract class IntegrationTestBase
   {
      private const string ConsolePath = @"C:\tmp";
      protected Mock<IBarcodeService> _barcodeServiceMock;
      protected Mock<IConsoleService> _consoleService;
      protected Mock<IDateTimeService> _dateTimeServiceMock;
      protected Mock<IFileService> _fileServiceMock;
      protected Mock<IOutputWriter> _outputWriterMock;
      protected Mock<IProcessService> _processServiceMock;
      protected Mock<IWebService> _webServiceMock;
      protected string _output;
      protected IExecutor _executor;

      [TestInitialize]
      public void Initialize()
      {
         var serviceCollection = new ServiceCollection();
         DependencyRegistration.RegisterDependencies(serviceCollection);

         _barcodeServiceMock = new Mock<IBarcodeService>();

         _consoleService = new Mock<IConsoleService>();
         _consoleService
            .Setup(m => m.GetConsolePath())
            .Returns(ConsolePath);

         _dateTimeServiceMock = new Mock<IDateTimeService>();

         _fileServiceMock = new Mock<IFileService>();
         _fileServiceMock
            .Setup(m => m.ReadAllText(Path.Combine(ConsolePath, "Resources/filetypes.json")))
            .Returns(@"[{""Extension"":""TXT"",""Description"":""Common name for ASCII text file"",""UsedBy"":""Microsoft Notepad""}]");
         _fileServiceMock
            .Setup(m => m.ReadAllText(Path.Combine(ConsolePath, "Resources/mime.json")))
            .Returns(@"{""pdf"":""application/pdf""}");

         _outputWriterMock = new Mock<IOutputWriter>();
         _outputWriterMock
            .Setup(m => m.WriteOutput(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
            .Callback<string, IDictionary<string, string>>((result, a) => _output = result);

         _processServiceMock = new Mock<IProcessService>();
         _webServiceMock = new Mock<IWebService>();

         serviceCollection.AddSingleton(_barcodeServiceMock.Object);
         serviceCollection.AddSingleton(_consoleService.Object);
         serviceCollection.AddSingleton(_dateTimeServiceMock.Object);
         serviceCollection.AddSingleton(_fileServiceMock.Object);
         serviceCollection.AddSingleton(_outputWriterMock.Object);
         serviceCollection.AddSingleton(_processServiceMock.Object);
         serviceCollection.AddSingleton(_webServiceMock.Object);

         var provider = serviceCollection.BuildServiceProvider();

         _executor = provider.GetService<IExecutor>();
      }

      [TestCleanup]
      public void Cleanup()
      {
         _barcodeServiceMock.VerifyAll();
         _dateTimeServiceMock.VerifyAll();
         _fileServiceMock.VerifyAll();
         _outputWriterMock.VerifyAll();
         _webServiceMock.VerifyAll();
      }

      protected string[] GetArgs(string input)
      {
         return input.Split(' ');
      }
   }
}
