using System.Collections.Generic;
using Armyknife.Business.Implementations;
using Armyknife.Business.Tools;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Business.Tests.Implementations
{
    [TestClass]
    public class ExecutorFacts
    {
        private Mock<IConsoleService> _consoleServiceMock;
        private Mock<IInputReader> _inputReaderMock;
        private Mock<IOutputWriter> _outputWriterMock;
        private Mock<IToolResolver> _toolResolverMock;
        private Executor _executor;

        [TestInitialize]
        public void Initialize()
        {
            _consoleServiceMock = new Mock<IConsoleService>();
            _inputReaderMock = new Mock<IInputReader>();
            _outputWriterMock = new Mock<IOutputWriter>();
            _toolResolverMock = new Mock<IToolResolver>();
            _executor = new Executor(
                _consoleServiceMock.Object,
                _inputReaderMock.Object,
                _outputWriterMock.Object,
                _toolResolverMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _consoleServiceMock.VerifyAll();
            _inputReaderMock.VerifyAll();
            _outputWriterMock.VerifyAll();
            _toolResolverMock.VerifyAll();
        }

        [TestMethod]
        public void Executor_Execute_ArgsNull_ShouldThrowException()
        {
            // arrange
            string[] args = null;

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _executor.Execute(args));

            // assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(ExceptionResources.NoArgs, exception.Message);
        }

        [TestMethod]
        public void Executor_Execute_ArgsEmpty_ShouldThrowException()
        {
            // arrange
            string[] args = new string[0];

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _executor.Execute(args));

            // assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(ExceptionResources.NoArgs, exception.Message);
        }

        [TestMethod]
        public void Executor_Execute_ShowGenericHelp()
        {
            // arrange
            string[] args = { Constants.HelpKey };

            _consoleServiceMock
                .Setup(m => m.WriteLine(It.IsAny<string>()));

            // act
            _executor.Execute(args);

            // assert
            _consoleServiceMock
                .Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Executor_Execute_ToolNotFound_ShouldThrowException()
        {
            // arrange
            string toolName = "testtool";
            string[] args = { toolName };
            string expectedExceptionMessage = string.Format(ExceptionResources.NoToolFoundMessage, toolName);

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns(null as ITool);

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _executor.Execute(args));

            // assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [TestMethod]
        public void Executor_Execute_ShowToolHelp()
        {
            // arrange
            string toolName = "testtool";
            string[] args = { toolName, Constants.HelpKey };
            string helpText = "this is the tool help text";

            var toolMock = new Mock<ITool>();

            toolMock
                .Setup(m => m.HelpText)
                .Returns(helpText);

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns(toolMock.Object);

            _consoleServiceMock
                .Setup(m => m.WriteLine(helpText));

            // act
            _executor.Execute(args);

            // assert
            _consoleServiceMock
                .Verify(m => m.WriteLine(helpText), Times.Once);
        }

        [TestMethod]
        public void Executor_Execute_InputReaderReturnsInput_ShouldBeAddedToArgsDictionary()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = "this is the input";

            var toolMock = new Mock<ITool>();

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns(toolMock.Object);

            _inputReaderMock
                .Setup(m => m.GetInput(args, It.IsAny<IDictionary<string, string>>()))
                .Callback((string[] passedArgs, IDictionary<string, string> passedArgsDictionary) =>
                {
                    actualArgsDictionary = passedArgsDictionary;
                })
                .Returns(input);

            // act
            _executor.Execute(args);

            // assert
            Assert.IsNotNull(actualArgsDictionary);
            Assert.AreEqual(1, actualArgsDictionary.Count);
            Assert.AreEqual(input, actualArgsDictionary[Constants.InputKey]);
        }

        [TestMethod]
        public void Executor_Execute_InputReaderReturnsNoInput_ArgsDictionaryShouldBeEmpty()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = string.Empty;

            var toolMock = new Mock<ITool>();

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns(toolMock.Object);

            _inputReaderMock
                .Setup(m => m.GetInput(args, It.IsAny<IDictionary<string, string>>()))
                .Callback((string[] passedArgs, IDictionary<string, string> passedArgsDictionary) =>
                {
                    actualArgsDictionary = passedArgsDictionary;
                })
                .Returns(input);

            // act
            _executor.Execute(args);

            // assert
            Assert.IsNotNull(actualArgsDictionary);
            Assert.AreEqual(0, actualArgsDictionary.Count);
        }

        [TestMethod]
        public void Executor_Execute_ResultIsSuccessfullyWrittenToOutput()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = string.Empty;
            byte[] output = { 1, 2, 3 };

            var toolMock = new Mock<ITool>();

            toolMock
                .Setup(m => m.Execute(It.IsAny<IDictionary<string, string>>()))
                .Returns(output);

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns(toolMock.Object);

            _inputReaderMock
                .Setup(m => m.GetInput(args, It.IsAny<IDictionary<string, string>>()))
                .Callback((string[] passedArgs, IDictionary<string, string> passedArgsDictionary) =>
                {
                    actualArgsDictionary = passedArgsDictionary;
                })
                .Returns(input);

            _outputWriterMock
                .Setup(m => m.WriteOutput(output, It.IsAny<IDictionary<string, string>>()));

            // act
            _executor.Execute(args);

            // assert
            Assert.IsNotNull(actualArgsDictionary);

            toolMock
                .Verify(m => m.Execute(It.IsAny<IDictionary<string, string>>()), Times.Once);

            _outputWriterMock
                .Verify(m => m.WriteOutput(output, It.IsAny<IDictionary<string, string>>()), Times.Once);
        }
    }
}
