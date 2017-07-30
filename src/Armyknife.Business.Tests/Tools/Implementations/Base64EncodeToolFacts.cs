using System;
using System.Collections.Generic;
using System.Text;
using Armyknife.Business.Tools.Implementations;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Business.Tests.Tools.Implementations
{
    [TestClass]
    public class Base64EncodeToolFacts
    {
        private Base64EncodeTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new Base64EncodeTool();
        }

        [TestMethod]
        public void Base64EncodeTool_Execute_NoInput_ShouldThrowArmyknifeException()
        {
            // arrange
            var argsDictionary = new Dictionary<string, string>();

            // act / assert
            Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
        }

        [TestMethod]
        public void Base64EncodeTool_Execute_NoInput_HappyFlow()
        {
            // arrange
            string input = "some basic input";
            string expectedOutput = "c29tZSBiYXNpYyBpbnB1dA==";
            var argsDictionary = new Dictionary<string, string>
            {
                { Constants.InputKey, input }
            };

            // act
            var result = _tool.Execute(argsDictionary);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedOutput, Encoding.UTF8.GetString(result));
        }
    }
}
