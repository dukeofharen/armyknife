using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class WordcountToolFacts
   {
      private WordcountTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new WordcountTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void WordcountTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void WordcountTool_Execute_HappyFlow_SingleLine()
      {
         // arrange
         string input = "count the number of words";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "5";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }

      [TestMethod]
      public void WordcountTool_Execute_HappyFlow_MultipleLines()
      {
         // arrange
         string input = "count the number\r\nof\nwords";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "5";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
