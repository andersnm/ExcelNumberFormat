using System.Globalization;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace ExcelNumberFormat.Tests
{
    [TestClass]
    public class RoundingTests
    {
        string Format(object value, string formatString, CultureInfo culture, bool isDate1904 = false)
        {
            var format = new NumberFormat(formatString);
            if (format.IsValid)
                return format.Format(value, culture, isDate1904);

            return null;
        }

        void Test(object value, string format, string expected, bool isDate1904 = false)
        {
            var result = Format(value, format, CultureInfo.InvariantCulture, isDate1904);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestRoundHalfAwayFromZero()
        {
            Test(2.5, "0", "3");
            Test(3.5, "0", "4");
            Test(-2.5, "0", "-3");
            Test(1234.5, "#,##0", "1,235");
            Test(0.125, "0.00", "0.13");
            Test(2.675, "0.00", "2.68");
            Test(0.125, "0.0%", "12.5%");
            Test(0.0125, "0.0%", "1.3%");
            Test(1.5, "0", "2");
            Test(0.4, "0", "0");
        }
    }
}
