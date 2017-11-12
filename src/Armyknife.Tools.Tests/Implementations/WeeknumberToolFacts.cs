using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Services.Interfaces;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Armyknife.Tools.Tests.Implementations
{
   [TestClass]
   public class WeeknumberToolFacts
   {
      private Mock<IDateTimeService> _dateTimeServiceMock;
      private WeeknumberTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _dateTimeServiceMock = new Mock<IDateTimeService>();
         _tool = new WeeknumberTool(_dateTimeServiceMock.Object);
      }

      [TestCleanup]
      public void Cleanup()
      {
         // Empty for now
      }

      [TestMethod]
      public void WeeknumberTool_Execute_HappyFlow()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();
         var now = new DateTime(2017, 11, 12);
         string expectedOutput = "45";

         _dateTimeServiceMock
            .Setup(m => m.Now)
            .Returns(now);

         // act
         string output = _tool.Execute(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
      }
   }
}
