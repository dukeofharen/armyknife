using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Tools.Implementations
{
    [TestClass]
    public class Sha256ToolFacts
    {
        private Sha256Tool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new Sha256Tool();
        }

        [TestMethod]
        public void Sha256Tool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void Sha256Tool_Execute_WrongOutputType_ShouldThrowException()
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
        public void Sha256Tool_Execute_NoOutputType_ShouldDefaultToHex()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "2eefbd527117fb0951426b86ac42b7cc61687f9512b09a06f09d60eabd75a37d";
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
        public void Sha256Tool_Execute_Hex_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "2eefbd527117fb0951426b86ac42b7cc61687f9512b09a06f09d60eabd75a37d";
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
        public void Sha256Tool_Execute_Base64_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "Lu+9UnEX+wlRQmuGrEK3zGFof5USsJoG8J1g6r11o30=";
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
        public void Sha256Tool_Execute_Hex_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "6142eee7b45260d99424caae9a1269d977b72ae87daf550dbf01c12c20b8d8c0";
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
        public void Sha256Tool_Execute_Base64_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "YULu57RSYNmUJMqumhJp2Xe3Kuh9r1UNvwHBLCC42MA=";
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
