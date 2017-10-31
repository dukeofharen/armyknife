using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Armyknife.Tools.Tests.Implementations
{
    [TestClass]
    public class LipsumToolFacts
    {
        private LipsumTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new LipsumTool();
        }

        [TestMethod]
        public void LipsumTool_Execute_HappyFlow()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var result = _tool.Execute(args);

            // assert
            Assert.IsNotNull(result);

            var parts = result.Split($"{Environment.NewLine}{Environment.NewLine}");
            Assert.AreEqual(5, parts.Length);
            Assert.IsTrue(parts[0].StartsWith("Lorem ipsum"));
        }

        [TestMethod]
        public void LipsumTool_Execute_HappyFlow_WithParagraphsParameter()
        {
            // arrange
            int paragraphs = 20;
            var args = new Dictionary<string, string>
            {
                { "paragraphs", paragraphs.ToString() }
            };

            // act
            var result = _tool.Execute(args);

            // assert
            Assert.IsNotNull(result);

            var parts = result.Split($"{Environment.NewLine}{Environment.NewLine}");
            Assert.AreEqual(paragraphs, parts.Length);
            Assert.IsTrue(parts[0].StartsWith("Lorem ipsum"));
        }
    }
}
