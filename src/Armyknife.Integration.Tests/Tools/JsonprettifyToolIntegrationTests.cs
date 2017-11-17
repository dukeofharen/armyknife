using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class JsonprettifyToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task JsonprettifyTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($@"jsonprettify --input {{""key"": ""value""}} --character space --tabsize 1");

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(3, _output.Split(Environment.NewLine).Length);
      }
   }
}
