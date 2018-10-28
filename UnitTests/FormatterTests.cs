using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameHelper;
using NSubstitute;

namespace UnitTests
{
    [TestClass]
    public class FormatterTests
    {
        [TestMethod]
        public void Process_ApplyRules_OnCall()
        {
            var rule = Substitute.For<IFormatRule>();
            var rules = new[] {rule};
            var formatter = new Formatter(rules);

            var args = new[] {"nAme oNe", "NAME TWO", "name three"};

           formatter.Process(args);

            rule.Received(args.Length).Apply(Arg.Any<string>());
        }

        [TestMethod]
        public void Process_JoinArgs_OnCall()
        {
            var args = new[] {"nAme oNe", "NAME TWO", "name three"};

            var result = new Formatter(new List<IFormatRule>()).Process(args);

            Assert.IsTrue(!string.IsNullOrEmpty(result));
            Assert.AreEqual(args.Length,
                result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length);
        }
    }
}