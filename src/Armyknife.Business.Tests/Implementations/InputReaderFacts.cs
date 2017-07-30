using System.Collections.Generic;
using Armyknife.Business.Implementations;
using Armyknife.Models;
using Armyknife.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Business.Tests.Implementations
{
    [TestClass]
    public class InputReaderFacts
    {
        private Mock<IFileService> _fileServiceMock;
        private InputReader _reader;

        [TestInitialize]
        public void Initialize()
        {
            _fileServiceMock = new Mock<IFileService>();
            _reader = new InputReader(_fileServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fileServiceMock.VerifyAll();
        }

        [TestMethod]
        public void InputReader_GetInput_RegularInput()
        {
            // arrange
            var argsDictionary = new Dictionary<string, string>();
            var args = new[] { "base64encode", "this", "is", "input" };
            string expectedInput = "this is input";

            // act
            string result = _reader.GetInput(args, argsDictionary);

            // assert
            Assert.AreEqual(expectedInput, result);
        }

        [TestMethod]
        public void InputReader_GetInput_FileInput()
        {
            // arrange
            string filePath = @"C:\tmp\file.txt";
            var argsDictionary = new Dictionary<string, string>
            {
                { Constants.FileInputKey, filePath }
            };
            string expectedInput = "these are the file contents";
            var args = new string[0];

            _fileServiceMock
                .Setup(m => m.ReadAllText(filePath))
                .Returns(expectedInput);

            // act
            string result = _reader.GetInput(args, argsDictionary);

            // assert
            Assert.AreEqual(expectedInput, result);
        }

        [TestMethod]
        public void InputReader_GetInput_NoInput()
        {
            // arrange
            string filePath = @"C:\tmp\file.txt";
            var argsDictionary = new Dictionary<string, string>
            {
                { "noInput", filePath }
            };
            string expectedInput = string.Empty;
            var args = new string[0];

            // act
            string result = _reader.GetInput(args, argsDictionary);

            // assert
            Assert.AreEqual(expectedInput, result);
        }
    }
}
