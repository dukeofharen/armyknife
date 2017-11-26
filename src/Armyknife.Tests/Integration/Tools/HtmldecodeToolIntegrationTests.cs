using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class HtmldecodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task HtmldecodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"htmldecode &lt;html&gt;&lt;/html&gt;");
         string expectedOutput = "<html></html>";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
