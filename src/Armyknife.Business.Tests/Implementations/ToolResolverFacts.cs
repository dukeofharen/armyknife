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

            var tool2 = new Mock<ITool>();
            tool2
                .Setup(m => m.Name)
                .Returns("tool2");

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
        public void ToolResolver_GetToolNames_HappyFlow()
        {
            // act
            var names = _resolver.GetToolNames().ToArray();

            // assert
            Assert.IsNotNull(names);
            Assert.AreEqual(_tools.Length, names.Length);
            Assert.AreEqual(_tools[0].Name, names[0]);
            Assert.AreEqual(_tools[1].Name, names[1]);
        }
    }
}
