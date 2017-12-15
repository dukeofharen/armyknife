using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
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
         await Executor.ExecuteAsync(args);

         // assert
         var parts = Output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
         Assert.AreEqual(10, parts.Length);
      }
   }
}
