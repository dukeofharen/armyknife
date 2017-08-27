using Armyknife.Business.Tools.Implementations;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Armyknife.Business.Tests.Tools.Implementations
{
    [TestClass]
    public class Sha384ToolFacts
    {
        private Sha384Tool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new Sha384Tool();
        }

        [TestMethod]
        public void Sha384Tool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void Sha384Tool_Execute_WrongOutputType_ShouldThrowException()
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
        public void Sha384Tool_Execute_NoOutputType_ShouldDefaultToHex()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "0b20a2cb786881e838b9a6b0349b2f75b63187e14fa8c494acadad89798e2c1f0aa385f99eb403b27d1134644267d724";
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
        public void Sha384Tool_Execute_Hex_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "0b20a2cb786881e838b9a6b0349b2f75b63187e14fa8c494acadad89798e2c1f0aa385f99eb403b27d1134644267d724";
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
        public void Sha384Tool_Execute_Base64_NoHmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "CyCiy3hogeg4uaawNJsvdbYxh+FPqMSUrK2tiXmOLB8Ko4X5nrQDsn0RNGRCZ9ck";
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
        public void Sha384Tool_Execute_Hex_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "61b04c0286e27e8de4fdb13dcd058c708403030a4f222afe44f1a0673e3cc2bf624ec8b656e5a2c95e3de3c985583106";
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
        public void Sha384Tool_Execute_Base64_Hmac()
        {
            // arrange
            string input = "this is the input";
            string expectedOutput = "YbBMAobifo3k/bE9zQWMcIQDAwpPIir+RPGgZz48wr9iTsi2VuWiyV4948mFWDEG";
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
