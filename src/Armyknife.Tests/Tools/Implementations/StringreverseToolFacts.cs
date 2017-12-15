using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class StringreverseToolFacts
   {
      private StringreverseTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new StringreverseTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void StringreverseTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void StringreverseTool_Execute_HappyFlow()
      {
         // arrange
         string input = "Reverse this string.";
         string expectedOutput = ".gnirts siht esreveR";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input}
         };

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
