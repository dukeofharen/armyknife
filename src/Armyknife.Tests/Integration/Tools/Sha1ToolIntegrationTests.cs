using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class Sha1ToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Sha1Tool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("sha1 --input this is the input --hmac secret key --outputType base64");
         string expectedOutput = "4+Q1Ybcwj4aofyGF2HpFBo+5uYI=";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
