using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class UuidToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task UuidTool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("uuid --brackets true --uppercase true --hyphens true --howmany 10");

         // act
         await _executor.ExecuteAsync(args);

         // assert
         var parts = _output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
         Assert.AreEqual(10, parts.Length);
      }
   }
}
