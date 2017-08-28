﻿using Armyknife.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;

namespace Armyknife.Services.Tests.Implementations
{
    [TestClass]
    public class MimeServiceFacts
    {
        private const string ConsolePath = @"C:\tmp";
        private const string MimeJson = @"{""xlsx"":""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"", ""txt"":""text/plain""}";
        private Mock<IConsoleService> _consoleServiceMock;
        private Mock<IFileService> _fileServiceMock;
        private MimeService _service;

        [TestInitialize]
        public void Initialize()
        {
            _consoleServiceMock = new Mock<IConsoleService>();
            _consoleServiceMock
                .Setup(m => m.GetConsolePath())
                .Returns(ConsolePath);

            string mimePath = Path.Combine(ConsolePath, "Resources/mime.json");

            _fileServiceMock = new Mock<IFileService>();
            _fileServiceMock
                .Setup(m => m.ReadAllText(mimePath))
                .Returns(MimeJson);

            _service = new MimeService(
                _consoleServiceMock.Object,
                _fileServiceMock.Object);
        }

        [TestMethod]
        public void MimeService_GetMimeType_HappyFlow()
        {
            // arrange
            string extension = "txt";
            string expectedMimeType = "text/plain";

            // act
            string result = _service.GetMimeType(extension);

            // assert
            Assert.AreEqual(expectedMimeType, result);
        }

        [TestMethod]
        public void MimeService_GetMimeType_HappyFlow_InputIsFilename()
        {
            // arrange
            string extension = "file.txt";
            string expectedMimeType = "text/plain";

            // act
            string result = _service.GetMimeType(extension);

            // assert
            Assert.AreEqual(expectedMimeType, result);
        }

        [TestMethod]
        public void MimeService_GetMimeType_HappyFlow_MimeTypeNotFound_ShouldReturnFallbackMimeType()
        {
            // arrange
            string extension = "blablabla";
            string expectedMimeType = "application/octet-stream";

            // act
            string result = _service.GetMimeType(extension);

            // assert
            Assert.AreEqual(expectedMimeType, result);
        }

        [TestMethod]
        public void MimeService_GetMimeType_HappyFlow_InputIsEmpty_ShouldReturnFallbackMimeType()
        {
            // arrange
            string extension = string.Empty;
            string expectedMimeType = "application/octet-stream";

            // act
            string result = _service.GetMimeType(extension);

            // assert
            Assert.AreEqual(expectedMimeType, result);
        }

        [TestMethod]
        public void MimeService_GetExtension_HappyFlow()
        {
            // arrange
            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string expectedExtension = "xlsx";

            // act
            var result = _service.GetExtensions(mimeType).ToArray();

            // assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(expectedExtension, result.First());
        }

        [TestMethod]
        public void MimeService_GetExtension_MimeTypeNotFound_ShouldReturnEmptyArray()
        {
            // arrange
            string mimeType = "text/unknown-mime";

            // act
            var result = _service.GetExtensions(mimeType).ToArray();

            // assert
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void MimeService_GetExtension_InputIsEmpty_ShouldReturnEmptyArray()
        {
            // arrange
            string mimeType = string.Empty;

            // act
            var result = _service.GetExtensions(mimeType).ToArray();

            // assert
            Assert.AreEqual(0, result.Length);
        }
    }
}
