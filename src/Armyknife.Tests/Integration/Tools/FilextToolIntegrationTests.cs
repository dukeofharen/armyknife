using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class FilextToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task FilextTool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("filext txt");
         string resultContains = "ASCII text file";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.IsTrue(_output.Contains(resultContains));
      }
   }
}
