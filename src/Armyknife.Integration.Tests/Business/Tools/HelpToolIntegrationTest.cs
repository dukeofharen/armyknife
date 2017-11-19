using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Business.Tools
{
   [TestClass]
   public class HelpToolIntegrationTest : IntegrationTestBase
   {
      [TestMethod]
      public async Task HelpTool_IntegrationTest_GenericHelp()
      {
         // arrange
         var args = GetArgs($"help");

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.IsTrue(_output.Split(Environment.NewLine).Length > 1);
      }

      [TestMethod]
      public async Task HelpTool_IntegrationTest_SpecificHelp()
      {
         // arrange
         var args = GetArgs($"help base64encode");

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.IsTrue(_output.Split(Environment.NewLine).Length > 1);
      }
   }
}
