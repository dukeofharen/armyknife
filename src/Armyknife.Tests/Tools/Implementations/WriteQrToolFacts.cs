using System;
using System.Collections.Generic;
using System.Text;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Tools.Implementations
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
      public void WriteQrTool_Execute_InputNotSet_ShouldThrowArmyknifeException()
      {
         // arrange
         var args = new Dictionary<string, string>();

         // act
         var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

         // assert
         Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
      }

      [TestMethod]
      public void WriteQrTool_Execute_ExtensionAndWriteLocationBothNotSet_ShouldThrowArmyknifeException()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };

         // act
         var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

         // assert
         Assert.AreEqual($"You should fill either '{Constants.FileOutputKey}' or 'extension'.", exception.Message);
      }

      [TestMethod]
      public void WriteQrTool_Execute_ExtensionAndWriteLocationBothSet_ShouldThrowArmyknifeException()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { Constants.FileOutputKey, Guid.NewGuid().ToString() },
            { "extension", Guid.NewGuid().ToString() }
         };

         // act
         var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

         // assert
         Assert.AreEqual($"You should fill either '{Constants.FileOutputKey}' or 'extension'.", exception.Message);
      }

      [TestMethod]
      public void WriteQrTool_Execute_WriteLocationSet_Png_HappyFlow()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         string writeLocation = "file.png";
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { Constants.FileOutputKey, writeLocation }
         };

         var pngBytes = new byte[] { 1, 2, 3 };

         _barcodeServiceMock
            .Setup(m => m.GenerateQrCodePng(input, 250, 250))
            .Returns(pngBytes);

         _fileServiceMock
            .Setup(m => m.WriteAllBytes(writeLocation, pngBytes));

         // act
         string result = _tool.Execute(args);

         // assert
         Assert.AreEqual(string.Empty, result);
      }

      [TestMethod]
      public void WriteQrTool_Execute_WriteLocationSet_Svg_HappyFlow()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         string writeLocation = "file.svg";
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { Constants.FileOutputKey, writeLocation }
         };

         var svgContents = "12345";

         _barcodeServiceMock
            .Setup(m => m.GenerateQrCodeSvg(input, 250, 250))
            .Returns(svgContents);

         _fileServiceMock
            .Setup(m => m.WriteAllText(writeLocation, svgContents));

         // act
         string result = _tool.Execute(args);

         // assert
         Assert.AreEqual(string.Empty, result);
      }

      [TestMethod]
      public void WriteQrTool_Execute_WriteLocationSet_Svg_HappyFlow_OpenFile()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         string writeLocation = "file.svg";
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { Constants.FileOutputKey, writeLocation },
            { "openFile", "true" }
         };

         var svgContents = "12345";

         _barcodeServiceMock
            .Setup(m => m.GenerateQrCodeSvg(input, 250, 250))
            .Returns(svgContents);

         _fileServiceMock
            .Setup(m => m.WriteAllText(writeLocation, svgContents));

         _processServiceMock
            .Setup(m => m.StartProcess(writeLocation));

         // act
         string result = _tool.Execute(args);

         // assert
         Assert.AreEqual(string.Empty, result);
      }

      [TestMethod]
      public void WriteQrTool_Execute_WriteLocationSet_UnknownExtension_ShouldThrowArmyknifeException()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         string writeLocation = "file.sjaak";
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { Constants.FileOutputKey, writeLocation }
         };

         // act
         var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

         // assert
         Assert.IsTrue(exception.Message.Contains("Saving QR code to a file"));
      }

      [TestMethod]
      public void WriteQrTool_Execute_ExtensionSet_Png_HappyFlow()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         string extension = "png";
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { "extension", extension }
         };

         var pngBytes = new byte[] { 1, 2, 3 };
         string expectedResult = Convert.ToBase64String(pngBytes);

         _barcodeServiceMock
            .Setup(m => m.GenerateQrCodePng(input, 250, 250))
            .Returns(pngBytes);

         // act
         string result = _tool.Execute(args);

         // assert
         Assert.AreEqual(expectedResult, result);
      }

      [TestMethod]
      public void WriteQrTool_Execute_ExtensionSet_Svg_HappyFlow()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         string extension = "svg";
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { "extension", extension }
         };

         var svgContents = "12345";
         string expectedResult = Convert.ToBase64String(Encoding.UTF8.GetBytes(svgContents));

         _barcodeServiceMock
            .Setup(m => m.GenerateQrCodeSvg(input, 250, 250))
            .Returns(svgContents);

         // act
         string result = _tool.Execute(args);

         // assert
         Assert.AreEqual(expectedResult, result);
      }

      [TestMethod]
      public void WriteQrTool_Execute_ExtensionSet_UnknownExtension_ShouldThrowArmyknifeException()
      {
         // arrange
         string input = Guid.NewGuid().ToString();
         string extension = "sjaak";
         var args = new Dictionary<string, string>
         {
            { Constants.InputKey, input },
            { "extension", extension }
         };

         // act
         var exception = Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));

         // assert
         Assert.IsTrue(exception.Message.Contains("Saving QR code to a file"));
      }
   }
}
