using System.Globalization;
using NUnit.Framework;

namespace ExcelNumberFormat.Tests
{
    [TestFixture]
    public class NonFiniteTests
    {
        static string Format(object value, string formatString)
        {
            return new NumberFormat(formatString).Format(value, CultureInfo.InvariantCulture);
        }

        [TestCase(double.NaN, "yyyy-mm-dd", "NaN")]
        [TestCase(double.PositiveInfinity, "yyyy-mm-dd hh:mm", "Infinity")]
        [TestCase(double.NegativeInfinity, "yyyy-mm-dd", "-Infinity")]
        [TestCase(1e300, "yyyy-mm-dd", "1E+300")]
        [TestCase(double.NaN, "0.00E+00", "NaN")]
        [TestCase(double.PositiveInfinity, "0.00E+00", "Infinity")]
        [TestCase(double.NaN, "# ?/?", "NaN")]
        [TestCase(double.NegativeInfinity, "# ?/?", "-Infinity")]
        [TestCase(double.PositiveInfinity, "[h]:mm", "Infinity")]
        public void NonFiniteAndOutOfRange(double value, string format, string expected)
        {
            Assert.AreEqual(expected, Format(value, format));
        }

        [Test]
        public void LargeDurationDoesNotWrap()
        {
            Assert.AreEqual("3000000000", Format(System.TimeSpan.FromSeconds(3000000000), "[s]"));
        }
    }
}
