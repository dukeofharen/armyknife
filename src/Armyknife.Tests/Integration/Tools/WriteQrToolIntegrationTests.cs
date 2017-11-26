using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class WriteQrToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task WriteQrTool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs(@"writeqr --input this is the QR content --outputFile C:\temp\qr.png --width 250 --height 250 --openFile true");

         _fileServiceMock
            .Setup(m => m.WriteAllBytes(It.IsAny<string>(), It.IsAny<byte[]>()));

         _barcodeServiceMock
            .Setup(m => m.GenerateQrCode(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(new byte[0]);

         _processServiceMock
            .Setup(m => m.StartProcess(It.IsAny<string>()));

         // act / assert
         await _executor.ExecuteAsync(args);
      }
   }
}
