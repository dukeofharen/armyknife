using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class Sha512ToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Sha512Tool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("sha512 --input this is the input --hmac secret key --outputType base64");
         string expectedOutput = "MwhMAI6YWka2/0EUbjXK7T2emho5fCVdesld45C6wEj4i07m55XaWfE+wHe+pjyQT5s8nLq7/03HUWXkt4YILw==";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
