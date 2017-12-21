using System.IO;
using Armyknife.Business;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Implementations;
using Armyknife.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Integration
{
   public abstract class IntegrationTestBase
   {
      private const string ConsolePath = @"C:\tmp";
      protected Mock<IBarcodeService> BarcodeServiceMock;
      protected Mock<IConsoleService> ConsoleService;
      protected Mock<IDateTimeService> DateTimeServiceMock;
      protected Mock<IFileService> FileServiceMock;
      protected Mock<IOutputWriter> OutputWriterMock;
      protected Mock<IProcessService> ProcessServiceMock;
      protected Mock<IWebService> WebServiceMock;
      protected string Output;
      protected IExecutor Executor;

      [TestInitialize]
      public void Initialize()
      {
         var serviceCollection = new ServiceCollection();
         var wrapper = new DnCoreServiceContainerWrapper(serviceCollection);
         DependencyRegistration.RegisterDependencies(wrapper);

         BarcodeServiceMock = new Mock<IBarcodeService>();

         ConsoleService = new Mock<IConsoleService>();
         ConsoleService
            .Setup(m => m.GetConsolePath())
            .Returns(ConsolePath);

         DateTimeServiceMock = new Mock<IDateTimeService>();

         FileServiceMock = new Mock<IFileService>();
         FileServiceMock
            .Setup(m => m.ReadAllText(Path.Combine(ConsolePath, "Resources/filetypes.json")))
            .Returns(@"[{""Extension"":""TXT"",""Description"":""Common name for ASCII text file"",""UsedBy"":""Microsoft Notepad""}]");
         FileServiceMock
            .Setup(m => m.ReadAllText(Path.Combine(ConsolePath, "Resources/mime.json")))
            .Returns(@"{""pdf"":""application/pdf""}");

         OutputWriterMock = new Mock<IOutputWriter>();
         OutputWriterMock
            .Setup(m => m.WriteOutput(It.IsAny<string>()))
            .Callback<string>(result => Output = result);

         ProcessServiceMock = new Mock<IProcessService>();
         WebServiceMock = new Mock<IWebService>();

         serviceCollection.AddSingleton(BarcodeServiceMock.Object);
         serviceCollection.AddSingleton(ConsoleService.Object);
         serviceCollection.AddSingleton(DateTimeServiceMock.Object);
         serviceCollection.AddSingleton(FileServiceMock.Object);
         serviceCollection.AddSingleton(OutputWriterMock.Object);
         serviceCollection.AddSingleton(ProcessServiceMock.Object);
         serviceCollection.AddSingleton(WebServiceMock.Object);

         wrapper.Provider = serviceCollection.BuildServiceProvider();

         Executor = wrapper.Resolve<IExecutor>();
      }

      [TestCleanup]
      public void Cleanup()
      {
         BarcodeServiceMock.VerifyAll();
         DateTimeServiceMock.VerifyAll();
         FileServiceMock.VerifyAll();
         OutputWriterMock.VerifyAll();
         WebServiceMock.VerifyAll();
      }

      protected string[] GetArgs(string input)
      {
         return input.Split(' ');
      }
   }
}
