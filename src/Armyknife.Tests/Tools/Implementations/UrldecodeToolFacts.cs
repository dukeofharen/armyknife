using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tools.Tests.Implementations
{
   [TestClass]
   public class UrldecodeToolFacts
   {
      private UrldecodeTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new UrldecodeTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void UrldecodeTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void UrldecodeTool_Execute_HappyFlow()
      {
         // arrange
         string input = "https%3a%2f%2fgoogle.com";
         string expectedOutput = "https://google.com";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
