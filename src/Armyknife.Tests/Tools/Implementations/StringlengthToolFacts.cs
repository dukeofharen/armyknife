using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class StringlengthToolFacts
   {
      private StringlengthTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new StringlengthTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void StringlengthTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void StringlengthTool_Execute_HappyFlow()
      {
         // arrange
         string input = "calculate the length of this string";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "35";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
