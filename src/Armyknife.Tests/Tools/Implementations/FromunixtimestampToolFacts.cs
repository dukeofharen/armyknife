using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class FromunixtimestampToolFacts
   {
      private FromunixtimestampTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new FromunixtimestampTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void FromunixtimestampTool_Execute_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void FromunixtimestampTool_Execute_InvalidInput_ShouldThrowArmyknifeException()
      {
         // arrange
         string input = "wrong timestamp";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };

         // act
         var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));

         // assert
         Assert.IsTrue(exception.Message.Contains("not a valid UNIX timestamp"));
      }

      [TestMethod]
      public void FromunixtimestampTool_Execute_TimestampWrongLength_ShouldThrowArmyknifeException()
      {
         // arrange
         string input = "151388922";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };

         // act
         var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));

         // assert
         Assert.IsTrue(exception.Message.Contains("should be either 10 or 13 characters long"));
      }

      [TestMethod]
      public void FromunixtimestampTool_Execute_HappyFlow_Seconds()
      {
         // arrange
         string input = "1513889220";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "2017-12-21 20:47:00:000";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }

      [TestMethod]
      public void FromunixtimestampTool_Execute_HappyFlow_Milliseconds()
      {
         // arrange
         string input = "1513889220332";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "2017-12-21 20:47:00:332";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
