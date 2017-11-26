using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class LipsumToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task LipsumTool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("lipsum --paragraphs 10");

         // act
         await _executor.ExecuteAsync(args);

         // assert
         var parts = _output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
         Assert.AreEqual(10, parts.Length);
      }
   }
}
