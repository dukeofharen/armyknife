using Armyknife.Business.Tools.Implementations;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Armyknife.Business.Tests.Tools.Implementations
{
    [TestClass]
    public class ToMimeToolFacts
    {
        private Mock<IMimeService> _mimeServiceMock;
        private ToMimeTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _mimeServiceMock = new Mock<IMimeService>();
            _tool = new ToMimeTool(_mimeServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mimeServiceMock.VerifyAll();
        }

        [TestMethod]
        public void ToMimeTool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void ToMimeTool_Execute_HappyFlow()
        {
            // arrange
            string filename = "file.txt";
            string mimeType = "text/plain";
            var args = new Dictionary<string, string>
            {
                { "input", filename }
            };

            _mimeServiceMock
                .Setup(m => m.GetMimeType(filename))
                .Returns(mimeType);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(mimeType, result);
        }
    }
}
