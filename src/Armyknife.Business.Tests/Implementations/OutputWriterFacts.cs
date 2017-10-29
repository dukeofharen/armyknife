using System;
using System.Collections.Generic;
using System.Text;
using Armyknife.Business.Implementations;
using Armyknife.Models;
using Armyknife.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

            var argsDictionary = new Dictionary<string, string>();

            // act
            _writer.WriteOutput(expectedResult, argsDictionary);

            // assert
            _consoleServiceMock
                .Verify(m => m.WriteLine(expectedResult), Times.Once);
        }
    }
}
