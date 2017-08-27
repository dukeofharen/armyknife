using Armyknife.Business.Tools.Implementations;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Armyknife.Business.Tests.Tools.Implementations
{
    [TestClass]
    public class Sha1ToolFacts
    {
        private Sha1Tool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new Sha1Tool();
        }

        [TestMethod]
        public void Sha1Tool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void Sha1Tool_Execute_WrongOutputType_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>
            {
                { "input", "this is the input" },
                { "outputType", "wrong" }
            };

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(string.Format(ExceptionResources.Sha1OutputTypeNotSupported, args["outputType"]), exception.Message);
        }

        [TestMethod]
        public void Sha1Tool_Execute_NoOutputType_ShouldDefaultToHex()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "828908f1a601a156b29b61fc3dde9bf8c2b49eb7";
            var args = new Dictionary<string, string>
            {
                { "input", input }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Sha1Tool_Execute_Hex_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "828908f1a601a156b29b61fc3dde9bf8c2b49eb7";
            var args = new Dictionary<string, string>
            {
                { "input", input },
                { "outputType", "hex" }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Sha1Tool_Execute_Base64_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "gokI8aYBoVaym2H8Pd6b+MK0nrc=";
            var args = new Dictionary<string, string>
            {
                { "input", input },
                { "outputType", "base64" }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Sha1Tool_Execute_Hex_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "e3e43561b7308f86a87f2185d87a45068fb9b982";
            string hmac = "secret key";
            var args = new Dictionary<string, string>
            {
                { "input", input },
                { "outputType", "hex" },
                { "hmac", hmac }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Sha1Tool_Execute_Base64_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "4+Q1Ybcwj4aofyGF2HpFBo+5uYI=";
            string hmac = "secret key";
            var args = new Dictionary<string, string>
            {
                { "input", input },
                { "outputType", "base64" },
                { "hmac", hmac }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(expectedOutput, result);
        }
    }
}
