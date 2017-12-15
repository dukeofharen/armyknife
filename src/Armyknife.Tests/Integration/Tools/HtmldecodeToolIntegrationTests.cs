using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class HtmldecodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task HtmldecodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("htmldecode &lt;html&gt;&lt;/html&gt;");
         string expectedOutput = "<html></html>";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
