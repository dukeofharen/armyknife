using System;
using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class RemovenewlineToolFacts
   {
      private RemovenewlineTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new RemovenewlineTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void RemovenewlineTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void RemovenewlineTool_Execute_HappyFlow()
      {
         // arrange
         string input = "piece\r\nof\ntext with new\r\nlines";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "pieceoftext with newlines";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
