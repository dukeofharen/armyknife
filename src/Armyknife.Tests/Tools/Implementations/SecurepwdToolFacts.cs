using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class SecurepwdToolFacts
   {
      private SecurepwdTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _tool = new SecurepwdTool();
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void SecurepwdTool_Execute_NothingFilledIn_ShouldCreatePasswordOf20CharactersLong()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(20, output.Length);
      }

      [TestMethod]
      public void SecurepwdTool_Execute_LengthFilledIn_ShouldCreatePasswordOfGivenCharactersLong()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>
         {
            {"length", "150"}
         };

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(150, output.Length);
      }

      [TestMethod]
      public void SecurepwdTool_Execute_NoCharactersAllowed_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>
         {
            { "capitals", "false" },
            { "lowercase", "false" },
            { "numbers", "false" },
            { "specialchars", "false" }
         };

         // act / assert
         Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
      }
   }
}
