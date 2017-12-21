using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class RemovenewlineToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task RemovenewlineTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("removenewline piece\r\nof\ntext with new\r\nlines");
         string expectedOutput = "pieceoftext with newlines";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
