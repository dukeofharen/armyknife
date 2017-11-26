using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class Md5ToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Md5Tool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("md5 --input this is the input --hmac secret key --outputType base64");
         string expectedOutput = "6O7myoK1Lgy74mSsOFvD6A==";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
