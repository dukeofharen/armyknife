using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class StringreverseToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task StringreverseTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"stringreverse Reverse this string.");
         string expectedOutput = ".gnirts siht esreveR";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
