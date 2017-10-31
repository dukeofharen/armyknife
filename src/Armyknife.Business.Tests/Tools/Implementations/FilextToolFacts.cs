using Armyknife.Business.Tools.Implementations;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Armyknife.Business.Tests.Tools.Implementations
{
    [TestClass]
    public class FilextToolFacts
    {
        private Mock<IFileExtensionService> _fileExtensionServiceMock;
        private FilextTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _fileExtensionServiceMock = new Mock<IFileExtensionService>();
            _tool = new FilextTool(_fileExtensionServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fileExtensionServiceMock.VerifyAll();
        }

        [TestMethod]
        public void FilextTool_Execute_NoInput_ShouldThrowException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void FilextTool_Execute_ExtensionNotFound_ShouldThrowArmyknifeException()
        {
            // arrange
            string extension = "txt";
            string expectedErrorMessage = string.Format(ExceptionResources.FilextExtensionNotFound, extension);
            var args = new Dictionary<string, string>
            {
                { "input", extension }
            };

            _fileExtensionServiceMock
                .Setup(m => m.GetFileExtensionInfo(extension))
                .Returns((FileExtensionInfoModel)null);

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }

        [TestMethod]
        public void FilextTool_Execute_HappyFlow()
        {
            // arrange
            string extension = "txt";
            var extensionInfo = new FileExtensionInfoModel
            {
                Extension = extension,
                Description = "fileDescription",
                UsedBy = "Extension used by"
            };
            string expectedResult = string.Format(ToolResources.FilextResult, extensionInfo.Extension, extensionInfo.Description, extensionInfo.UsedBy);
            var args = new Dictionary<string, string>
            {
                { "input", extension }
            };

            _fileExtensionServiceMock
                .Setup(m => m.GetFileExtensionInfo(extension))
                .Returns(extensionInfo);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
