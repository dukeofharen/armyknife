using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
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
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
