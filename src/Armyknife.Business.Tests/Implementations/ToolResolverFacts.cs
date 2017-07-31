using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Armyknife.Business.Implementations;
using Armyknife.Business.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Business.Tests.Implementations
{
    [TestClass]
    public class ToolResolverFacts
    {
        private Mock<IServiceProvider> _serviceProviderMock;
        private ToolResolver _resolver;
        private ITool[] _tools;

        [TestInitialize]
        public void Initialize()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();

            var tool1 = new Mock<ITool>();
            tool1
                .Setup(m => m.Name)
                .Returns("tool1");
            tool1
                .Setup(m => m.Category)
                .Returns("category1");
            tool1
                .Setup(m => m.Description)
                .Returns("description1");
            tool1
                .Setup(m => m.HelpText)
                .Returns("help1");

            var tool2 = new Mock<ITool>();
            tool2
                .Setup(m => m.Name)
                .Returns("tool2");
            tool2
                .Setup(m => m.Category)
                .Returns("category2");
            tool2
                .Setup(m => m.Description)
                .Returns("description2");
            tool2
                .Setup(m => m.HelpText)
                .Returns("help2");

            _tools = new[]
            {
                tool1.Object,
                tool2.Object
            };

            _serviceProviderMock
                .Setup(m => m.GetService(typeof(IEnumerable<ITool>)))
                .Returns(_tools);

            _resolver = new ToolResolver(_serviceProviderMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceProviderMock.VerifyAll();
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

            Assert.AreEqual("tool2", result[1].Key);
            Assert.AreEqual("category2", result[1].Category);
            Assert.AreEqual("description2", result[1].ShortDescription);
            Assert.AreEqual("help2", result[1].HelpText);
        }
    }
}
