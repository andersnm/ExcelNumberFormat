using System.Globalization;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace ExcelNumberFormat.Tests
{
    [TestClass]
    public class NumericTypeTests
    {
        void Test(object value, string format, string expected)
        {
            var result = new NumberFormat(format).Format(value, CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOtherNumericTypes()
        {
            Test((byte)200, "#,##0", "200");
            Test((sbyte)-5, "0.0", "-5.0");
            Test((ushort)1234, "#,##0", "1,234");
            Test(1234u, "#,##0", "1,234");
            Test(1234L, "#,##0", "1,234");
            Test(12345678901234UL, "#,##0", "12,345,678,901,234");
            Test(1234.5f, "#,##0.00", "1,234.50");
            Test(1234m, "#,##0", "1,234");
            Test(1234.5m, "#,##0.00", "1,234.50");
            Test(0.25m, "0%", "25%");
            Test(-1234L, "#,##0;(#,##0)", "(1,234)");
            Test(45498L, "yyyy-mm-dd", "2024-07-25");
            Test(0L, "#,##0;-#,##0;\"zero\"", "zero");
        }

        [TestMethod]
        public void TestFloat()
        {
            Test(123456792f, "0", "123456792");
            Test(1234.56f, "0.00", "1234.56");
            Test(1234.5678f, "0.0000", "1234.5677");
            Test(float.MaxValue, "General", "3.402823E+38");
        }
    }
}
