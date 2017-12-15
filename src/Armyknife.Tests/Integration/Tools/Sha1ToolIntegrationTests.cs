using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
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
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
