using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PrefixFormatRuleTests
    {
        [TestMethod]
        public void Apply_FormatVon()
        {
            var rule = new PrefixFormatRule.PrefixFormatRule("von");
            var result = rule.Apply("Otto Von Bismarck");
            Assert.AreEqual("Otto von Bismarck", result);
        }

        [TestMethod]
        public void Apply_FormatMc()
        {
            var rule = new PrefixFormatRule.PrefixFormatRule("Mc");
            var result = rule.Apply("Tramp Mcdonald");
            Assert.AreEqual("Tramp McDonald", result);
        }
    }
}