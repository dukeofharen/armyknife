using Armyknife.Business.Interfaces;
using Armyknife.Business.Tools;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Armyknife.Business.Tests.Tools
{
    [TestClass]
    public class HelpToolFacts
    {
        private Mock<IAssemblyService> _assemblyServiceMock;
        private Mock<IToolResolver> _toolResolverMock;
        private HelpTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _assemblyServiceMock = new Mock<IAssemblyService>();
            _toolResolverMock = new Mock<IToolResolver>();
            _tool = new HelpTool(
                _assemblyServiceMock.Object,
                _toolResolverMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _assemblyServiceMock.VerifyAll();
            _toolResolverMock.VerifyAll();
        }

        [TestMethod]
        public void HelpTool_Execute_ShowToolHelp_ToolNotFound_ShouldThrowArmyknifeException()
        {
            // arrange
            string toolName = "testtool";
            var args = new Dictionary<string, string>
            {
                { Constants.InputKey, toolName }
            };

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns((ITool)null);

            // act / assert
            Assert.ThrowsException<ArmyknifeException>(() => _tool.Execute(args));
        }

        [TestMethod]
        public void HelpTool_Execute_ShowToolHelp_HappyFlow()
        {
            // arrange
            string toolName = "testtool";
            string toolDescription = "This is a test tool.";
            string toolHelp = "Use the tool like this.";
            var args = new Dictionary<string, string>
            {
                { Constants.InputKey, toolName }
            };

            var toolMock = new Mock<ITool>();
            toolMock
                .Setup(m => m.Description)
                .Returns(toolDescription);
            toolMock
                .Setup(m => m.HelpText)
                .Returns(toolHelp);

            _toolResolverMock
                .Setup(m => m.ResolveTool(toolName))
                .Returns(toolMock.Object);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.IsTrue(result.Contains(toolDescription));
            Assert.IsTrue(result.Contains(toolHelp));
        }

        [TestMethod]
        public void HelpTool_Execute_ShowGenericHelp_HappyFlow()
        {
            // arrange
            var args = new Dictionary<string, string>();
            var toolMetaData = new[]
            {
                new ToolMetaDataModel
                {
                    Category = "category1",
                    HelpText = "help1",
                    Key = "tool1",
                    ShortDescription = "description1",
                    ShowToolInHelp = true
                },
                new ToolMetaDataModel
                {
                    Category = "category1",
                    HelpText = "help2",
                    Key = "tool2",
                    ShortDescription = "description2",
                    ShowToolInHelp = true
                },
                new ToolMetaDataModel
                {
                    Category = "category2",
                    HelpText = "help3",
                    Key = "tool3",
                    ShortDescription = "description3",
                    ShowToolInHelp = false
                }
            };

            _toolResolverMock
                .Setup(m => m.GetToolMetData())
                .Returns(toolMetaData);

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(1, result.Split("category1").Length - 1);

            Assert.IsTrue(result.Contains("tool1"));
            Assert.IsTrue(result.Contains("description1"));

            Assert.IsTrue(result.Contains("tool2"));
            Assert.IsTrue(result.Contains("description2"));

            Assert.IsFalse(result.Contains("category2"));
        }
    }
}
