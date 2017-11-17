using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tools.Tests.Implementations
{
   [TestClass]
   public class JsonprettifyToolFacts
   {
      private JsonprettifyTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new JsonprettifyTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void JsonprettifyTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void JsonprettifyTool_Execute_HappyFlow_DefaultValues()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, @"{""key"": ""value""}" }
         };

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         var parts = output.Split(Environment.NewLine);
         Assert.AreEqual(3, parts.Length);
         Assert.IsTrue(parts[1].Contains("   "));
      }

      [TestMethod]
      public void JsonprettifyTool_Execute_HappyFlow_CustomValues()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, @"{""key"": ""value""}" },
            { "character", "tab" },
            { "tabsize", "2" }
         };

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         var parts = output.Split(Environment.NewLine);
         Assert.AreEqual(3, parts.Length);
         Assert.IsTrue(parts[1].Contains("\t\t"));
      }
   }
}
