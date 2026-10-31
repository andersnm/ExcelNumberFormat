using System.Globalization;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace ExcelNumberFormat.Tests
{
    [TestClass]
    public class TextSectionTests
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
        public void TestTextSectionLiterals()
        {
            Test("abc", "\"Prefix: \"@", "Prefix: abc");
            Test("abc", "\"x\"@\"y\"", "xabcy");
            Test("abc", "0;-0;0;\"Note: \"@", "Note: abc");
            Test("abc", "0;\"Text: \"@", "Text: abc");
            Test("abc", "0;-0;\"Text: \"@", "Text: abc");
            Test("abc", "@", "abc");
            Test("abc", "0;-0;0", "abc");
            Test("abc", "0.00", "abc");
        }
    }
}
