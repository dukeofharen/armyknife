using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class Sha256ToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Sha256Tool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("sha256 --input this is the input --hmac secret key --outputType base64");
         string expectedOutput = "YULu57RSYNmUJMqumhJp2Xe3Kuh9r1UNvwHBLCC42MA=";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
