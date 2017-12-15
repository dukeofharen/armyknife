using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Tools.Implementations
{
    [TestClass]
    public class Md5ToolFacts
    {
        private Md5Tool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new Md5Tool();
        }

        [TestMethod]
        public void Md5Tool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void Md5Tool_Execute_WrongOutputType_ShouldThrowException()
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
            Assert.AreEqual(string.Format(ExceptionResources.Md5OutputTypeNotSupported, args["outputType"]), exception.Message);
        }

        [TestMethod]
        public void Md5Tool_Execute_NoOutputType_ShouldDefaultToHex()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "e4397781321a09219dedfc612bb1bc23";
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
        public void Md5Tool_Execute_Hex_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "e4397781321a09219dedfc612bb1bc23";
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
        public void Md5Tool_Execute_Base64_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "5Dl3gTIaCSGd7fxhK7G8Iw==";
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
        public void Md5Tool_Execute_Hex_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "e8eee6ca82b52e0cbbe264ac385bc3e8";
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
        public void Md5Tool_Execute_Base64_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "6O7myoK1Lgy74mSsOFvD6A==";
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
