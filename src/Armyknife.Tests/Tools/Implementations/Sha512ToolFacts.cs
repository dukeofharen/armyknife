using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Tools.Implementations
{
    [TestClass]
    public class Sha512ToolFacts
    {
        private Sha512Tool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new Sha512Tool();
        }

        [TestMethod]
        public void Sha512Tool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void Sha512Tool_Execute_WrongOutputType_ShouldThrowException()
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
        public void Sha512Tool_Execute_NoOutputType_ShouldDefaultToHex()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "ac31b17bade95abf171363aa3b51af670e76ad49429b517b299e6048af19b7f57f06ab9af0c185871cc29feb5c562c19a0cb70542f3a6ad19be0dedb30018b8e";
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
        public void Sha512Tool_Execute_Hex_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "ac31b17bade95abf171363aa3b51af670e76ad49429b517b299e6048af19b7f57f06ab9af0c185871cc29feb5c562c19a0cb70542f3a6ad19be0dedb30018b8e";
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
        public void Sha512Tool_Execute_Base64_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "rDGxe63pWr8XE2OqO1GvZw52rUlCm1F7KZ5gSK8Zt/V/Bqua8MGFhxzCn+tcViwZoMtwVC86atGb4N7bMAGLjg==";
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
        public void Sha512Tool_Execute_Hex_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "33084c008e985a46b6ff41146e35caed3d9e9a1a397c255d7ac95de390bac048f88b4ee6e795da59f13ec077bea63c904f9b3c9cbabbff4dc75165e4b786082f";
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
        public void Sha512Tool_Execute_Base64_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "MwhMAI6YWka2/0EUbjXK7T2emho5fCVdesld45C6wEj4i07m55XaWfE+wHe+pjyQT5s8nLq7/03HUWXkt4YILw==";
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
