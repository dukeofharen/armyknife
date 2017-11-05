using System;
using System.Collections.Generic;
using Armyknife.Business.Implementations;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;

namespace Armyknife.Business.Tests.Implementations
{
    [TestClass]
    public class ExecutorFacts
    {
        private Mock<IAssemblyService> _assemblyServiceMock;
        private Mock<IConsoleService> _consoleServiceMock;
        private Mock<IInputReader> _inputReaderMock;
        private Mock<IOutputWriter> _outputWriterMock;
        private Mock<IToolResolver> _toolResolverMock;
        private Executor _executor;

        [TestInitialize]
        public void Initialize()
        {
            _assemblyServiceMock = new Mock<IAssemblyService>();
            _consoleServiceMock = new Mock<IConsoleService>();
            _inputReaderMock = new Mock<IInputReader>();
            _outputWriterMock = new Mock<IOutputWriter>();
            _toolResolverMock = new Mock<IToolResolver>();
            _executor = new Executor(
                _assemblyServiceMock.Object,
                _consoleServiceMock.Object,
                _inputReaderMock.Object,
                _outputWriterMock.Object,
                _toolResolverMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _assemblyServiceMock.VerifyAll();
            _consoleServiceMock.VerifyAll();
            _inputReaderMock.VerifyAll();
            _outputWriterMock.VerifyAll();
            _toolResolverMock.VerifyAll();
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_ArgsNull_ShouldShowHelp()
        {
            // arrange
            string[] args = null;
            string helpText = "This is the help text.";

            var toolMock = new Mock<ISynchronousTool>();
            toolMock
                .Setup(m => m.Execute(It.IsAny<IDictionary<string, string>>()))
                .Returns(helpText);

            _toolResolverMock
                .Setup(m => m.ResolveTool(Constants.HelpKey))
                .Returns(toolMock.Object);

            // act
            await _executor.ExecuteAsync(args);

            // assert
            _outputWriterMock
                .Verify(m => m.WriteOutput(helpText, It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_ArgsEmpty_ShouldShowHelp()
        {
            // arrange
            string[] args = new string[0];
            string helpText = "This is the help text.";

            var toolMock = new Mock<ISynchronousTool>();
            toolMock
                .Setup(m => m.Execute(It.IsAny<IDictionary<string, string>>()))
                .Returns(helpText);

            _toolResolverMock
                .Setup(m => m.ResolveTool(Constants.HelpKey))
                .Returns(toolMock.Object);

            // act
            await _executor.ExecuteAsync(args);

            // assert
            _outputWriterMock
                .Verify(m => m.WriteOutput(helpText, It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_ShowGenericHelp()
        {
            // arrange
            string[] args = { Constants.HelpKey };
            string helpText = "This is the help text.";

            var toolMock = new Mock<ISynchronousTool>();
            toolMock
                .Setup(m => m.Execute(It.IsAny<IDictionary<string, string>>()))
                .Returns(helpText);

            _toolResolverMock
                .Setup(m => m.ResolveTool(Constants.HelpKey))
                .Returns(toolMock.Object);

            // act
            await _executor.ExecuteAsync(args);

            // assert
            _outputWriterMock
                .Verify(m => m.WriteOutput(helpText, It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_ToolNotFound_ShouldThrowException()
        {
            // arrange
            string toolName = "testtool";
            string[] args = { toolName };
            string expectedExceptionMessage = string.Format(ExceptionResources.NoToolFoundMessage, toolName);

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns(null as ITool);

            // act
            var exception = await Assert.ThrowsExceptionAsync<ArmyknifeException>(() => _executor.ExecuteAsync(args));

            // assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_InputReaderReturnsInput_ShouldBeAddedToArgsDictionary()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = "this is the input";

            var toolMock = new Mock<ISynchronousTool>();

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
            await _executor.ExecuteAsync(args);

            // assert
            Assert.IsNotNull(actualArgsDictionary);
            Assert.AreEqual(1, actualArgsDictionary.Count);
            Assert.AreEqual(input, actualArgsDictionary[Constants.InputKey]);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_InputReaderReturnsNoInput_ArgsDictionaryShouldBeEmpty()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = string.Empty;

            var toolMock = new Mock<ISynchronousTool>();

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
            await _executor.ExecuteAsync(args);

            // assert
            Assert.IsNotNull(actualArgsDictionary);
            Assert.AreEqual(0, actualArgsDictionary.Count);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_SynchronousTool_ResultIsSuccessfullyWrittenToOutput()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = string.Empty;
            string output = "123";

            var toolMock = new Mock<ISynchronousTool>();

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
            await _executor.ExecuteAsync(args);

            // assert
            Assert.IsNotNull(actualArgsDictionary);

            toolMock
                .Verify(m => m.Execute(It.IsAny<IDictionary<string, string>>()), Times.Once);

            _outputWriterMock
                .Verify(m => m.WriteOutput(output, It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_AsynchronousTool_ResultIsSuccessfullyWrittenToOutput()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = string.Empty;
            string output = "123";

            var toolMock = new Mock<IAsynchronousTool>();

            toolMock
                .Setup(m => m.ExecuteAsync(It.IsAny<IDictionary<string, string>>()))
                .ReturnsAsync(output);

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
            await _executor.ExecuteAsync(args);

            // assert
            Assert.IsNotNull(actualArgsDictionary);

            toolMock
                .Verify(m => m.ExecuteAsync(It.IsAny<IDictionary<string, string>>()), Times.Once);

            _outputWriterMock
                .Verify(m => m.WriteOutput(output, It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [TestMethod]
        public async Task Executor_ExecuteAsync_OtherTool_ShouldThrowInvalidOperationException()
        {
            // arrange
            IDictionary<string, string> actualArgsDictionary = null;
            string toolName = "testtool";
            string[] args = { toolName };
            string input = string.Empty;
            string output = "123";

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
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _executor.ExecuteAsync(args));

            // assert
            Assert.IsNotNull(actualArgsDictionary);

            _outputWriterMock
                .Verify(m => m.WriteOutput(output, It.IsAny<IDictionary<string, string>>()), Times.Never);
        }
    }
}
