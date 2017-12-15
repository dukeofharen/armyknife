using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Business.Tools
{
   [TestClass]
   public class HelpToolIntegrationTest : IntegrationTestBase
   {
      [TestMethod]
      public async Task HelpTool_IntegrationTest_GenericHelp()
      {
         // arrange
         var args = GetArgs("help");

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.IsTrue(Output.Split(Environment.NewLine).Length > 1);
      }

      [TestMethod]
      public async Task HelpTool_IntegrationTest_SpecificHelp()
      {
         // arrange
         var args = GetArgs("help base64encode");

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.IsTrue(Output.Split(Environment.NewLine).Length > 1);
      }
   }
}
