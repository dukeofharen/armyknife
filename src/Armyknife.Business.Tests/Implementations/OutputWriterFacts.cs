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
        public void OutputWriter_OutputToFile_NoSlashes_ShouldPrependWorkingDirectory()
        {
            // arrange
            string path = "output.txt";
            string workingDirectory = @"C:\tmp";
            string expectedPath = $@"{workingDirectory}\{path}";
            string expectedResult = "123";

            var argsDictionary = new Dictionary<string, string>
            {
                { Constants.FileOutputKey, path }
            };

            _consoleServiceMock
                .Setup(m => m.GetConsolePath())
                .Returns(workingDirectory);

            _fileServiceMock
                .Setup(m => m.WriteAllText(expectedPath, expectedResult));

            // act
            _writer.WriteOutput(expectedResult, argsDictionary);

            // assert
            _consoleServiceMock
                .Verify(m => m.GetConsolePath(), Times.Once);

            _fileServiceMock
                .Verify(m => m.WriteAllText(expectedPath, expectedResult), Times.Once);
        }

        [TestMethod]
        public void OutputWriter_OutputToFile_HasBackSlash_ShouldNotPrependWorkingDirectory()
        {
            // arrange
            string path = @"C:\tmp\output.txt";
            string expectedResult = "123";

            var argsDictionary = new Dictionary<string, string>
            {
                { Constants.FileOutputKey, path }
            };

            _fileServiceMock
                .Setup(m => m.WriteAllText(path, expectedResult));

            // act
            _writer.WriteOutput(expectedResult, argsDictionary);

            // assert
            _consoleServiceMock
                .Verify(m => m.GetConsolePath(), Times.Never);

            _fileServiceMock
                .Verify(m => m.WriteAllText(path, expectedResult), Times.Once);
        }

        [TestMethod]
        public void OutputWriter_OutputToFile_HasForwardSlash_ShouldNotPrependWorkingDirectory()
        {
            // arrange
            string path = "/var/output.txt";
            string expectedResult = "123";

            var argsDictionary = new Dictionary<string, string>
            {
                { Constants.FileOutputKey, path }
            };

            _fileServiceMock
                .Setup(m => m.WriteAllText(path, expectedResult));

            // act
            _writer.WriteOutput(expectedResult, argsDictionary);

            // assert
            _consoleServiceMock
                .Verify(m => m.GetConsolePath(), Times.Never);

            _fileServiceMock
                .Verify(m => m.WriteAllText(path, expectedResult), Times.Once);
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
