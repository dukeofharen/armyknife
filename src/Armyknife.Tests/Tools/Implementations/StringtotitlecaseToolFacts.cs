using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class StringtotitlecaseToolFacts
   {
      private StringtotitlecaseTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new StringtotitlecaseTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void StringtotitlecaseTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void StringtotitlecaseTool_Execute_HappyFlow()
      {
         // arrange
         string input = "CONVERT to title case";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "CONVERT To Title Case";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
