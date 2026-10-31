using System;
using System.Globalization;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace ExcelNumberFormat.Tests
{
    [TestClass]
    public class FractionalSecondsTests
    {
        void Test(object value, string format, string expected)
        {
            var numberFormat = new NumberFormat(format);
            Assert.IsTrue(numberFormat.IsValid);
            Assert.AreEqual(expected, numberFormat.Format(value, CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestFractionalSeconds()
        {
            // Expected values were checked against Excel 16, which shows as many digits as there are zeros and rounds them
            Test(12.34 / 86400, "mm:ss.0", "00:12.3");
            Test(12.34 / 86400, "mm:ss.00", "00:12.34");
            Test(12.345 / 86400, "mm:ss.00", "00:12.35");
            Test(12.35 / 86400, "mm:ss.0", "00:12.4");
            Test(588.34 / 86400, "mm:ss.0", "09:48.3");
            Test(12.3456 / 86400, "mm:ss.000", "00:12.346");
            Test(0.5 / 86400, "mm:ss.0", "00:00.5");

            // Stored just below .25, so Excel rounds down
            Test(3661.25 / 86400, "h:mm:ss.0", "1:01:01.2");
            Test(3661.26 / 86400, "h:mm:ss.0", "1:01:01.3");
            Test(3661.26 / 86400, "h:mm:ss.00", "1:01:01.26");

            // Rounding carries into seconds, minutes and hours
            Test(59.96 / 86400, "mm:ss.0", "01:00.0");
            Test(59.996 / 86400, "mm:ss.00", "01:00.00");
            Test(59.96 / 86400, "ss.0", "00.0");
            Test(3599.96 / 86400, "mm:ss.0", "00:00.0");

            Test(45498.123456, "mm:ss.00", "57:46.60");
            Test(45498.123456, "yyyy-mm-dd hh:mm:ss.0", "2024-07-25 02:57:46.6");
            Test(new DateTime(2024, 7, 25, 2, 57, 46, 598), "hh:mm:ss.0", "02:57:46.6");
            Test(new DateTime(2024, 7, 25, 2, 57, 59, 960), "hh:mm:ss.0", "02:58:00.0");
        }

        [TestMethod]
        public void TestFractionalSecondsDuration()
        {
            Test(1.96 / 86400, "[ss].0", "02.0");
            Test(125.76 / 86400, "[mm]:ss.0", "02:05.8");
            Test(125.76 / 86400, "[h]:mm:ss.0", "0:02:05.8");
            Test(new TimeSpan(0, 0, 2, 5, 760), "[mm]:ss.00", "02:05.76");
            Test(new TimeSpan(0, 0, 2, 5, 760), "[mm]:ss.0", "02:05.8");
            Test(new TimeSpan(0, 0, 0, 59, 960), "[mm]:ss.0", "01:00.0");
        }
    }
}
