using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Armyknife.Tools.Tests.Implementations
{
    [TestClass]
    public class UuidToolFacts
    {
        private UuidTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _tool = new UuidTool();
        }

        [TestMethod]
        public void UuidTool_Execute_NoArgs_ShouldReturn1Uuid()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(1, result.Split(Environment.NewLine).Length);
            Assert.IsTrue(result.Contains("-"));
            Assert.IsFalse(result.Contains("{") || result.Contains("}"));
            Assert.IsFalse(result.Any(c => char.IsUpper(c)));
        }

        [TestMethod]
        public void UuidTool_Execute_HowManyFilledIn_ShouldReturnThatNumberOfUuids()
        {
            // arrange
            int howMany = 10;
            var args = new Dictionary<string, string>
            {
                { "howmany", howMany.ToString() }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(howMany, result.Split(Environment.NewLine).Length);
        }

        [TestMethod]
        public void UuidTool_Execute_BracketsTrue_ShouldReturnUuidWithBrackets()
        {
            // arrange
            var args = new Dictionary<string, string>
            {
                { "brackets", "true" }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(1, result.Split(Environment.NewLine).Length);
            Assert.IsTrue(result.Contains("-"));
            Assert.IsTrue(result.Contains("{") || result.Contains("}"));
            Assert.IsFalse(result.Any(c => char.IsUpper(c)));
        }

        [TestMethod]
        public void UuidTool_Execute_UppercaseTrue_ShouldReturnUppercaseUuid()
        {
            // arrange
            var args = new Dictionary<string, string>
            {
                { "uppercase", "true" }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(1, result.Split(Environment.NewLine).Length);
            Assert.IsTrue(result.Contains("-"));
            Assert.IsFalse(result.Contains("{") || result.Contains("}"));
            Assert.IsTrue(result.Any(c => char.IsUpper(c)));
        }

        [TestMethod]
        public void UuidTool_Execute_HyphensFalse_ShouldReturnUuidWithoutHyphens()
        {
            // arrange
            var args = new Dictionary<string, string>
            {
                { "hyphens", "false" }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(1, result.Split(Environment.NewLine).Length);
            Assert.IsFalse(result.Contains("-"));
            Assert.IsFalse(result.Contains("{") || result.Contains("}"));
            Assert.IsFalse(result.Any(c => char.IsUpper(c)));
        }

        [TestMethod]
        public void UuidTool_Execute_EverythingFilledIn_ShouldReturnSeveralUuidsWithGivenProperties()
        {
            // arrange
            int howMany = 10;
            var args = new Dictionary<string, string>
            {
                { "howmany", howMany.ToString() },
                { "brackets", "true" },
                { "uppercase", "true" },
                { "hyphens", "true" }
            };

            // act
            string result = _tool.Execute(args);

            // assert
            Assert.AreEqual(howMany, result.Split(Environment.NewLine).Length);
            Assert.IsTrue(result.Contains("-"));
            Assert.IsTrue(result.Contains("{") || result.Contains("}"));
            Assert.IsTrue(result.Any(c => char.IsUpper(c)));
        }
    }
}
