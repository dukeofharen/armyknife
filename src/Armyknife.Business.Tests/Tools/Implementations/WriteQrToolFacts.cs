using Armyknife.Business.Tools.Implementations;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Armyknife.Business.Tests.Tools.Implementations
{
    [TestClass]
    public class WriteQrToolFacts
    {
        private Mock<IBarcodeService> _barcodeServiceMock;
        private Mock<IFileService> _fileServiceMock;
        private Mock<IProcessService> _processServiceMock;
        private WriteQrTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _barcodeServiceMock = new Mock<IBarcodeService>();
            _fileServiceMock = new Mock<IFileService>();
            _processServiceMock = new Mock<IProcessService>();
            _tool = new WriteQrTool(
                _barcodeServiceMock.Object,
                _fileServiceMock.Object,
                _processServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _barcodeServiceMock.VerifyAll();
            _fileServiceMock.VerifyAll();
            _processServiceMock.VerifyAll();
        }

        [TestMethod]
        public void WriteQrTool_Execute_OutputFileNotSet_ShouldThrowArmyknifeException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual("You should provide the 'outputFile'.", exception.Message);
        }

        [TestMethod]
        public void WriteQrTool_Execute_InputSet_ShouldThrowArmyknifeException()
        {
            // arrange
            string outputFile = @"C:\temp\qr.png";
            var args = new Dictionary<string, string>
            {
                { "outputFile", outputFile }
            };

            // act
            var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public void WriteQrTool_Execute_NoOtherArgumensSet_ShouldUseDefaultValues()
        {
            // arrange
            string outputFile = @"C:\temp\qr.png";
            string input = "QR this!";
            var qrBytes = new byte[] { 1, 2, 3 };
            var args = new Dictionary<string, string>
            {
                { "outputFile", outputFile },
                { "input", input }
            };

            _barcodeServiceMock
                .Setup(m => m.GenerateQrCode(input, 250, 250))
                .Returns(qrBytes);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
            _fileServiceMock.Verify(m => m.WriteAllBytes(outputFile, qrBytes), Times.Once);
            _processServiceMock.Verify(m => m.StartProcess(outputFile), Times.Never);
        }

        [TestMethod]
        public void WriteQrTool_Execute_WidthFilledIn()
        {
            // arrange
            string outputFile = @"C:\temp\qr.png";
            string input = "QR this!";
            var qrBytes = new byte[] { 1, 2, 3 };
            int width = 500;
            var args = new Dictionary<string, string>
            {
                { "outputFile", outputFile },
                { "input", input },
                { "width", width.ToString() }
            };

            _barcodeServiceMock
                .Setup(m => m.GenerateQrCode(input, 250, width))
                .Returns(qrBytes);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
            _fileServiceMock.Verify(m => m.WriteAllBytes(outputFile, qrBytes), Times.Once);
            _processServiceMock.Verify(m => m.StartProcess(outputFile), Times.Never);
        }

        [TestMethod]
        public void WriteQrTool_Execute_HeightFilledIn()
        {
            // arrange
            string outputFile = @"C:\temp\qr.png";
            string input = "QR this!";
            var qrBytes = new byte[] { 1, 2, 3 };
            int height = 500;
            var args = new Dictionary<string, string>
            {
                { "outputFile", outputFile },
                { "input", input },
                { "height", height.ToString() }
            };

            _barcodeServiceMock
                .Setup(m => m.GenerateQrCode(input, height, 250))
                .Returns(qrBytes);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
            _fileServiceMock.Verify(m => m.WriteAllBytes(outputFile, qrBytes), Times.Once);
            _processServiceMock.Verify(m => m.StartProcess(outputFile), Times.Never);
        }

        [TestMethod]
        public void WriteQrTool_Execute_OpenFileFilledIn()
        {
            // arrange
            string outputFile = @"C:\temp\qr.png";
            string input = "QR this!";
            var qrBytes = new byte[] { 1, 2, 3 };
            var args = new Dictionary<string, string>
            {
                { "outputFile", outputFile },
                { "input", input },
                { "openFile", "true" }
            };

            _barcodeServiceMock
                .Setup(m => m.GenerateQrCode(input, 250, 250))
                .Returns(qrBytes);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
            _fileServiceMock.Verify(m => m.WriteAllBytes(outputFile, qrBytes), Times.Once);
            _processServiceMock.Verify(m => m.StartProcess(outputFile), Times.Once);
        }

        [TestMethod]
        public void WriteQrTool_Execute_EverythingFilledIn()
        {
            // arrange
            string outputFile = @"C:\temp\qr.png";
            string input = "QR this!";
            var qrBytes = new byte[] { 1, 2, 3 };
            int width = 500;
            int height = 600;
            var args = new Dictionary<string, string>
            {
                { "outputFile", outputFile },
                { "input", input },
                { "width", width.ToString() },
                { "height", height.ToString() },
                { "openFile", "true" }
            };

            _barcodeServiceMock
                .Setup(m => m.GenerateQrCode(input, height, width))
                .Returns(qrBytes);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
            _fileServiceMock.Verify(m => m.WriteAllBytes(outputFile, qrBytes), Times.Once);
            _processServiceMock.Verify(m => m.StartProcess(outputFile), Times.Once);
        }
    }
}
