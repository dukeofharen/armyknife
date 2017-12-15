using System.Collections.Generic;
using Armyknife.Business.Implementations;
using Armyknife.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Business.Implementations
{
    [TestClass]
    public class InputReaderFacts
    {
        private Mock<IConsoleService> _consoleServiceMock;
        private InputReader _reader;

        [TestInitialize]
        public void Initialize()
        {
            _consoleServiceMock = new Mock<IConsoleService>();
            _reader = new InputReader(_consoleServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _consoleServiceMock.VerifyAll();
        }

        [TestMethod]
        public void InputReader_GetInput_PipedInput()
        {
            // arrange
            var argsDictionary = new Dictionary<string, string>();
            var args = new[] { "base64encode" };
            string expectedInput = "this is input";

            _consoleServiceMock
                .Setup(m => m.ReadPipedData())
                .Returns(expectedInput);

            // act
            string result = _reader.GetInput(args, argsDictionary);

            // assert
            Assert.AreEqual(expectedInput, result);
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
