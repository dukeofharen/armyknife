using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class WriteQrToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task WriteQrTool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs(@"writeqr --input this is the QR content --outputFile C:\temp\qr.png --width 250 --height 250 --openFile true");

         FileServiceMock
            .Setup(m => m.WriteAllBytes(It.IsAny<string>(), It.IsAny<byte[]>()));

         BarcodeServiceMock
            .Setup(m => m.GenerateQrCodePng(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(new byte[0]);

         ProcessServiceMock
            .Setup(m => m.StartProcess(It.IsAny<string>()));

         // act / assert
         await Executor.ExecuteAsync(args);
      }
   }
}
