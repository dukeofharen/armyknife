using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Armyknife.Tools.Tests.Implementations
{
    [TestClass]
    public class FromMimeToolFacts
    {
        private Mock<IMimeService> _mimeServiceMock;
        private FromMimeTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _mimeServiceMock = new Mock<IMimeService>();
            _tool = new FromMimeTool(_mimeServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mimeServiceMock.VerifyAll();
        }

        [TestMethod]
        public void FromMimeTool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void FromMimeTool_Execute_NoResult_ShouldThrowArmyknifeException()
        {
            // arrange
            string mimeType = "text/bla";
            var args = new Dictionary<string, string>
            {
                { "input", mimeType }
            };

            _mimeServiceMock
                .Setup(m => m.GetExtensions(mimeType))
                .Returns(new string[0]);

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreNotEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void FromMimeTool_Execute_HappyFlow()
        {
            // arrange
            string mimeType = "text/plain";
            var args = new Dictionary<string, string>
            {
                { "input", mimeType }
            };
            var extensions = new[] { "txt", "conf", "ini" };
            string expectedResult = "txt, conf, ini";

            _mimeServiceMock
                .Setup(m => m.GetExtensions(mimeType))
                .Returns(extensions);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
