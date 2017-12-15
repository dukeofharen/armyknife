using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Tools.Implementations
{
    [TestClass]
    public class Base64DecodeToolFacts
    {
        private Base64DecodeTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new Base64DecodeTool();
        }

        [TestMethod]
        public void Base64DecodeTool_Execute_NoInput_ShouldThrowArmyknifeException()
        {
            // arrange
            var argsDictionary = new Dictionary<string, string>();

            // act / assert
            Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(argsDictionary));
        }

        [TestMethod]
        public void Base64DecodeTool_Execute_HappyFlow()
        {
            // arrange
            string input = "c29tZSBiYXNpYyBpbnB1dA==";
            string expectedOutput = "some basic input";
            var argsDictionary = new Dictionary<string, string>
            {
                { Constants.InputKey, input }
            };

            // act
            var result = _tool.Execute(argsDictionary);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedOutput, result);
        }
    }
}
