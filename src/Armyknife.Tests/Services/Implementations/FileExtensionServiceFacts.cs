using Armyknife.Services;
using Armyknife.Services.Implementations;
using Armyknife.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Services.Implementations
{
    [TestClass]
    public class FileExtensionServiceFacts
    {
        private FileExtensionService _service;

        [TestInitialize]
        public void Initialize()
        {
            var serviceCollection = new ServiceCollection();
            DependencyRegistration.RegisterDependencies(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _service = (FileExtensionService)serviceProvider.GetService<IFileExtensionService>();
        }

        [TestMethod]
        public void FileExtensionService_GetFileExtensionInfo_HappyFlow()
        {
            // arrange
            string extension = "TXT";

            // act
            var result = _service.GetFileExtensionInfo(extension);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TXT", result.Extension);
            Assert.AreEqual("Common name for ASCII text file", result.Description);
            Assert.AreEqual("Microsoft Notepad", result.UsedBy);
        }

        [TestMethod]
        public void FileExtensionService_GetFileExtensionInfo_HappyFlow_ExtensionLowerCase()
        {
            // arrange
            string extension = "txt";

            // act
            var result = _service.GetFileExtensionInfo(extension);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TXT", result.Extension);
            Assert.AreEqual("Common name for ASCII text file", result.Description);
            Assert.AreEqual("Microsoft Notepad", result.UsedBy);
        }

        [TestMethod]
        public void FileExtensionService_GetFileExtensionInfo_NotFound_ShouldReturnNull()
        {
            // arrange
            string extension = "ragsdfaeswtygaerg";

            // act
            var result = _service.GetFileExtensionInfo(extension);

            // assert
            Assert.IsNull(result);
        }
    }
}
