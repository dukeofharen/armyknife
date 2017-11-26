using System.Collections.Generic;
using Armyknife.Business.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Armyknife.Services.Interfaces;

namespace Armyknife.Business.Tests.Implementations
{
    [TestClass]
    public class OutputWriterFacts
    {
        private Mock<IConsoleService> _consoleServiceMock;
        private Mock<IFileService> _fileServiceMock;
        private OutputWriter _writer;

        [TestInitialize]
        public void Initialize()
        {
            _consoleServiceMock = new Mock<IConsoleService>();
            _fileServiceMock = new Mock<IFileService>();
            _writer = new OutputWriter(
                _consoleServiceMock.Object,
                _fileServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _consoleServiceMock.VerifyAll();
            _fileServiceMock.VerifyAll();
        }

        [TestMethod]
        public void OutputWriter_OutputToConsole()
        {
            // arrange
            string expectedResult = "123";

            // act
            _writer.WriteOutput(expectedResult);

            // assert
            _consoleServiceMock
                .Verify(m => m.WriteLine(expectedResult), Times.Once);
        }
    }
}
