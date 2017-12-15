using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
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
         await Executor.ExecuteAsync(args);

         // assert
         var parts = Output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
         Assert.AreEqual(10, parts.Length);
      }
   }
}
