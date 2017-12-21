using System.Linq;
using Armyknife.DI.DnCore;
using Armyknife.Services;
using Armyknife.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Services.Implementations
{
   [TestClass]
   public class MimeServiceFacts
   {
      private IMimeService _service;

      [TestInitialize]
      public void Initialize()
      {
         var serviceCollection = new ServiceCollection();
         var wrapper = new DnCoreServiceContainerWrapper(serviceCollection);
         DependencyRegistration.RegisterDependencies(wrapper);
         wrapper.Provider = serviceCollection.BuildServiceProvider();
         _service = wrapper.Resolve<IMimeService>();
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
