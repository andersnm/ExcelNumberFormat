using System.Globalization;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace ExcelNumberFormat.Tests
{
    [TestClass]
    public class EmptySectionTests
    {
        void Test(object value, string formatString, string expected)
        {
            var format = new NumberFormat(formatString);
            Assert.IsTrue(format.IsValid, formatString);
            Assert.AreEqual(expected, format.Format(value, CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestEmptySections()
        {
            // Expected values were checked against Excel 16
            Test(-5, "0;;0", "");
            Test(0, "0;;0", "0");
            Test(-5, "0;", "");
            Test(0, "0;;", "");
            Test(5, ";;;", "");
            Test(-5, ";;;", "");
            Test(0, ";;;", "");
            Test("abc", ";;;", "");
            Test(-5, "0.00;;\"zero\"", "");
            Test(0, "0.00;;\"zero\"", "zero");
            Test(5, "0.00;;\"zero\"", "5.00");
        }

        [TestCase("[<]0")]
        [TestCase("[>-]0")]
        [TestCase("[=.]0")]
        public void TestConditionWithoutNumberIsInvalid(string formatString)
        {
            var format = new NumberFormat(formatString);
            Assert.IsFalse(format.IsValid);
        }

        [TestMethod]
        public void TestFractionConstantDenominatorTooLarge()
        {
            // Does not throw from the constructor
            var format = new NumberFormat("# ?/99999999999");
            Assert.IsFalse(format.IsValid);
        }
    }
}
