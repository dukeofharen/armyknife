using Armyknife.Business.Implementations;
using Armyknife.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Business.Implementations
{
    [TestClass]
    public class OutputWriterFacts
    {
        private Mock<IConsoleService> _consoleServiceMock;
        private OutputWriter _writer;

        [TestInitialize]
        public void Initialize()
        {
            _consoleServiceMock = new Mock<IConsoleService>();
            _writer = new OutputWriter(_consoleServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _consoleServiceMock.VerifyAll();
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
