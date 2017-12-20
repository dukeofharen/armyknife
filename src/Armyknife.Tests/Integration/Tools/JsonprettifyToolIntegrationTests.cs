using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class JsonprettifyToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task JsonprettifyTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs(@"jsonprettify --input {""key"": ""value""} --character space --tabsize 1");

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(3, Output.Split(Environment.NewLine).Length);
      }
   }
}
