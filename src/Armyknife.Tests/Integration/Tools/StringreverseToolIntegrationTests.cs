using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class StringreverseToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task StringreverseTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("stringreverse Reverse this string.");
         string expectedOutput = ".gnirts siht esreveR";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
