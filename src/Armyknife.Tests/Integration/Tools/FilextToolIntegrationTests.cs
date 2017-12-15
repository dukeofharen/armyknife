using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
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
         await Executor.ExecuteAsync(args);

         // assert
         Assert.IsTrue(Output.Contains(resultContains));
      }
   }
}
