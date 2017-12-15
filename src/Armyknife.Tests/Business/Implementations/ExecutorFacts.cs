using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Armyknife.Business.Implementations;
using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Business.Implementations
{
   [TestClass]
   public class ExecutorFacts
   {
      private Mock<IInputReader> _inputReaderMock;
      private Logger _logger;
      private Mock<IOutputWriter> _outputWriterMock;
      private Mock<IToolResolver> _toolResolverMock;
      private Executor _executor;

      [TestInitialize]
      public void Initialize()
      {
         _inputReaderMock = new Mock<IInputReader>();
         _logger = new Logger();
         _outputWriterMock = new Mock<IOutputWriter>();
         _toolResolverMock = new Mock<IToolResolver>();
         _executor = new Executor(
             _inputReaderMock.Object,
             _logger,
             _outputWriterMock.Object,
             _toolResolverMock.Object);
      }

      [TestCleanup]
      public void Cleanup()
      {
         _inputReaderMock.VerifyAll();
         _outputWriterMock.VerifyAll();
         _toolResolverMock.VerifyAll();
      }

      [TestMethod]
      public async Task Executor_ExecuteAsync_ArgsNull_ShouldShowHelp()
      {
         // arrange
         string helpText = "This is the help text.";

         var toolMock = new Mock<ISynchronousTool>();
         toolMock
             .Setup(m => m.Execute(It.IsAny<IDictionary<string, string>>()))
             .Returns(helpText);

         _toolResolverMock
             .Setup(m => m.ResolveTool(Constants.HelpKey))
             .Returns(toolMock.Object);

         // act
         int result = await _executor.ExecuteAsync(null);

         // assert
         _outputWriterMock
             .Verify(m => m.WriteOutput(helpText), Times.Once);

         Assert.AreEqual(0, result);
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
         int result = await _executor.ExecuteAsync(args);

         // assert
         _outputWriterMock
             .Verify(m => m.WriteOutput(helpText), Times.Once);

         Assert.AreEqual(0, result);
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
         int result = await _executor.ExecuteAsync(args);

         // assert
         _outputWriterMock
             .Verify(m => m.WriteOutput(helpText), Times.Once);

         Assert.AreEqual(0, result);
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
         int result = await _executor.ExecuteAsync(args);

         // assert
         Assert.IsTrue(_logger.GetLogMessages().Any(m => m.Contains(expectedExceptionMessage)));
         Assert.AreEqual(-1, result);
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
         int result = await _executor.ExecuteAsync(args);

         // assert
         Assert.IsNotNull(actualArgsDictionary);
         Assert.AreEqual(1, actualArgsDictionary.Count);
         Assert.AreEqual(input, actualArgsDictionary[Constants.InputKey]);
         Assert.AreEqual(0, result);
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
         int result = await _executor.ExecuteAsync(args);

         // assert
         Assert.IsNotNull(actualArgsDictionary);
         Assert.AreEqual(0, actualArgsDictionary.Count);
         Assert.AreEqual(0, result);
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
             .Setup(m => m.WriteOutput(output));

         // act
         int result = await _executor.ExecuteAsync(args);

         // assert
         Assert.IsNotNull(actualArgsDictionary);

         toolMock
             .Verify(m => m.Execute(It.IsAny<IDictionary<string, string>>()), Times.Once);

         _outputWriterMock
             .Verify(m => m.WriteOutput(output), Times.Once);

         Assert.AreEqual(0, result);
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
             .Setup(m => m.WriteOutput(output));

         // act
         int result = await _executor.ExecuteAsync(args);

         // assert
         Assert.IsNotNull(actualArgsDictionary);

         toolMock
             .Verify(m => m.ExecuteAsync(It.IsAny<IDictionary<string, string>>()), Times.Once);

         _outputWriterMock
             .Verify(m => m.WriteOutput(output), Times.Once);

         Assert.AreEqual(0, result);
      }

      [TestMethod]
      public async Task Executor_ExecuteAsync_DebugMode_ShouldPrintLogMessagesInConsole()
      {
         // arrange
         string actualOutput = null;
         string toolName = "testtool";
         string[] args = { toolName, "--debug" };
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
             .Returns(input);

         _outputWriterMock
             .Setup(m => m.WriteOutput(It.IsAny<string>()))
             .Callback<string>(o => actualOutput = o);

         // act
         int result = await _executor.ExecuteAsync(args);

         // assert
         toolMock
             .Verify(m => m.Execute(It.IsAny<IDictionary<string, string>>()), Times.Once);

         _outputWriterMock
             .Verify(m => m.WriteOutput(It.IsAny<string>()), Times.Once);

         foreach(string message in _logger.GetLogMessages())
         {
            Assert.IsTrue(actualOutput.Contains(message));
         }

         Assert.AreEqual(0, result);
      }

      [TestMethod]
      public async Task Executor_ExecuteAsync_ArmyknifeExceptionIsThrown_ShouldLogException()
      {
         // arrange
         string toolName = "testtool";
         string[] args = { toolName };
         string expectedOutput = "ERROR!";

         _toolResolverMock
             .Setup(m => m.ResolveTool(toolName))
             .Throws(new ArmyknifeException(expectedOutput));

         _outputWriterMock
             .Setup(m => m.WriteOutput(expectedOutput));

         // act
         int result = await _executor.ExecuteAsync(args);

         // assert
         _outputWriterMock
             .Verify(m => m.WriteOutput(expectedOutput), Times.Once);
         Assert.AreEqual(-1, result);
      }

      [TestMethod]
      public async Task Executor_ExecuteAsync_OtherExceptionIsThrown_ShouldLogException()
      {
         // arrange
         string toolName = "testtool";
         string[] args = { toolName };
         string exceptionText = "ERROR!";
         string expectedOutput = string.Format(GenericResources.SomethingWentWrong, toolName, exceptionText, GenericResources.GithubUrl);

         _toolResolverMock
             .Setup(m => m.ResolveTool(toolName))
             .Throws(new InvalidOperationException(exceptionText));

         _outputWriterMock
             .Setup(m => m.WriteOutput(expectedOutput));

         // act
         int result = await _executor.ExecuteAsync(args);

         // assert
         _outputWriterMock
             .Verify(m => m.WriteOutput(expectedOutput), Times.Once);
         Assert.AreEqual(-1, result);
      }

      [TestMethod]
      public async Task Executor_ExecuteAsync_OtherTool_ShouldThrowInvalidOperationException()
      {
         // arrange
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
             .Returns(input);

         // act
         int result = await _executor.ExecuteAsync(args);

         // assert
         _outputWriterMock
             .Verify(m => m.WriteOutput(output), Times.Never);
         Assert.IsTrue(_logger.GetLogMessages().Any(m => m.Contains("InvalidOperationException")));
         Assert.AreEqual(-1, result);
      }
   }
}
