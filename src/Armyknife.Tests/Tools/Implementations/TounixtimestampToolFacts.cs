using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class TounixtimestampToolFacts
   {
      private TounixtimestampTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new TounixtimestampTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void TounixtimestampTool_Execute_NoInputGiven_ShouldPickCurrentDateTime()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.IsFalse(string.IsNullOrEmpty(output));
      }

      [TestMethod]
      public void TounixtimestampTool_Execute_InvalidDateTime_ShouldThrowArmyknifeException()
      {
         // arrange
         string input = "bla";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }

      [TestMethod]
      public void TounixtimestampTool_Execute_Iso8601_Test1()
      {
         // arrange
         string input = "2017-08-20T15:00:00Z";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "1503241200000";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }

      [TestMethod]
      public void TounixtimestampTool_Execute_Iso8601_Test2()
      {
         // arrange
         string input = "2017-08-20T15:00:00+02:00";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "1503234000000";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }

      [TestMethod]
      public void TounixtimestampTool_Execute_Iso8601_Test3()
      {
         // arrange
         string input = "2017-08-20T15:00:00.443+02:00";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };
         string expectedOutput = "1503234000443";

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
