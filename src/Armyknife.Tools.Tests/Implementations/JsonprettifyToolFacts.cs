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
      public void JsonprettifyTool_Execute_HappyFlow()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, Guid.NewGuid().ToString() }
         };

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(string.Empty, output);
      }
   }
}