using System.Collections.Generic;
using System.Linq;
using Armyknife.Business.Implementations;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Business.Implementations
{
    [TestClass]
    public class ToolResolverFacts
    {
        private Mock<IServiceContainerWrapper> _wrapperMock;
        private ToolResolver _resolver;
        private ITool[] _tools;

        [TestInitialize]
        public void Initialize()
        {
            _wrapperMock = new Mock<IServiceContainerWrapper>();

            var tool1 = SetupTool("tool1", "category1", "description1", "help1", false);
            var tool2 = SetupTool("tool2", "category2", "description2", "help2", true);

            _tools = new[]
            {
                tool1.Object,
                tool2.Object
            };

            _wrapperMock
                .Setup(m => m.ResolveMultiple<ITool>())
                .Returns(_tools);

            _resolver = new ToolResolver(_wrapperMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _wrapperMock.VerifyAll();
        }

        [TestMethod]
        public void ToolResolver_ResolveTool_HappyFlow()
        {
            // arrange
            string toolName = "tool1";

            // act
            var tool = _resolver.ResolveTool(toolName);

            // assert
            Assert.IsNotNull(tool);
            Assert.AreEqual(toolName, tool.Name);
        }

        [TestMethod]
        public void ToolResolver_GetToolMetData_HappyFlow()
        {
            // act
            var result = _resolver.GetToolMetData().ToArray();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_tools.Length, result.Length);

            Assert.AreEqual("tool1", result[0].Key);
            Assert.AreEqual("category1", result[0].Category);
            Assert.AreEqual("description1", result[0].ShortDescription);
            Assert.AreEqual("help1", result[0].HelpText);
            Assert.AreEqual(false, result[0].ShowToolInHelp);

            Assert.AreEqual("tool2", result[1].Key);
            Assert.AreEqual("category2", result[1].Category);
            Assert.AreEqual("description2", result[1].ShortDescription);
            Assert.AreEqual("help2", result[1].HelpText);
            Assert.AreEqual(true, result[1].ShowToolInHelp);
        }

        private Mock<ITool> SetupTool(string name, string category, string description, string help, bool showInHelp)
        {
            var tool = new Mock<ITool>();
            tool
                .Setup(m => m.Name)
                .Returns(name);
            tool
                .Setup(m => m.Category)
                .Returns(category);
            tool
                .Setup(m => m.Description)
                .Returns(description);
            tool
                .Setup(m => m.HelpText)
                .Returns(help);
            tool
                .Setup(m => m.ShowToolInHelp)
                .Returns(showInHelp);

            return tool;
        }
    }
}
