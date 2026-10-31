using System;
using System.Globalization;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

// The expected values were produced by Excel 16 with the same format strings.

namespace ExcelNumberFormat.Tests
{
    [TestClass]
    public class EraTests
    {
        const string Full = "[$-411]ggge\"年\"m\"月\"d\"日\";@";

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
        public void TestJapaneseEraWithKanji()
        {
            // https://github.com/andersnm/ExcelNumberFormat/issues/39
            Test(44259, Full, "令和3年3月4日");
            Test(new DateTime(1900, 3, 1), Full, "明治33年3月1日");
            Test(new DateTime(1912, 7, 29), Full, "明治45年7月29日");
            Test(new DateTime(1912, 7, 30), Full, "大正1年7月30日");
            Test(new DateTime(1926, 12, 24), Full, "大正15年12月24日");
            Test(new DateTime(1926, 12, 25), Full, "昭和1年12月25日");
            Test(new DateTime(1989, 1, 7), Full, "昭和64年1月7日");
            Test(new DateTime(1989, 1, 8), Full, "平成1年1月8日");
            Test(new DateTime(2019, 4, 30), Full, "平成31年4月30日");
            Test(new DateTime(2019, 5, 1), Full, "令和1年5月1日");
        }

        [TestMethod]
        public void TestJapaneseEraNames()
        {
            var date = new DateTime(2021, 3, 4);
            Test(date, "[$-411]g", "R");
            Test(date, "[$-411]gg", "令");
            Test(date, "[$-411]ggg", "令和");
            Test(date, "[$-411]gggg", "令和");
            Test(new DateTime(1912, 7, 30), "[$-411]g", "T");
            Test(new DateTime(1926, 12, 25), "[$-411]gg", "昭");
            Test(new DateTime(1989, 1, 8), "[$-411]g", "H");
            Test(new DateTime(1900, 3, 1), "[$-411]g", "M");
        }

        [TestMethod]
        public void TestJapaneseEraYear()
        {
            Test(44259, @"[$-411]ge\.m\.d;@", "R3.3.4");
            Test(new DateTime(2019, 5, 1), @"[$-411]ge\.m\.d;@", "R1.5.1");
            Test(new DateTime(2021, 3, 4), "[$-411]e", "3");
            Test(new DateTime(2021, 3, 4), "[$-411]ee", "03");
            Test(new DateTime(2021, 3, 4), "[$-411]eee", "03");
            Test(new DateTime(1912, 7, 30), "[$-411]ee", "01");
            Test(new DateTime(1989, 1, 7), "[$-411]e", "64");
            Test(new DateTime(2021, 3, 4), "[$-411]yyyy e", "2021 3");
            Test(new DateTime(2021, 3, 4), "[$-411]yy e", "21 3");
            Test(new DateTime(2021, 3, 4), "[$-411]h:mm ge", "0:00 R3");
            Test(new DateTime(2021, 3, 4), "[$-411]gggee", "令和03");
        }

        [TestMethod]
        public void TestEraYearBeforeSeparator()
        {
            // "e-" and "e+" are exponents in a number, but an era year and a literal in a date
            Test(new DateTime(2021, 3, 4), "[$-411]e-m-d", "3-3-4");
            Test(new DateTime(2021, 3, 4), "[$-411]e+m", "3+3");
            Test(new DateTime(2021, 3, 4), "[$-411]e/m/d", "3/3/4");
            Test(new DateTime(2021, 3, 4), "e-m-d", "2021-3-4");
        }

        [TestMethod]
        public void TestEraYearOutsideJapanese()
        {
            // Without the Japanese locale e and ee print the four digit year
            Test(new DateTime(2021, 3, 4), "e", "2021");
            Test(new DateTime(2021, 3, 4), "ee", "2021");
            Test(new DateTime(2021, 3, 4), "[$-409]e", "2021");
            Test(new DateTime(2021, 3, 4), "[$-409]ee", "2021");
            Test(new DateTime(2021, 3, 4), "e/m/d", "2021/3/4");
        }

        [TestMethod]
        public void TestEraYearIsDate()
        {
            Assert.IsTrue(new NumberFormat("[$-411]ge.m.d").IsDateTimeFormat);
            Assert.IsTrue(new NumberFormat("e").IsDateTimeFormat);
        }

        [TestMethod]
        public void TestUpperCaseEraYearIsInvalid()
        {
            // Excel rejects an upper case E as a date code
            Assert.IsFalse(new NumberFormat("[$-411]E").IsValid);
        }

        [TestMethod]
        public void TestScientificFormatsAreUnchanged()
        {
            Test(12345.678, "0.00E+00", "1.23E+04");
            Test(0.000123, "0.00E-00", "1.23E-04");
        }
    }
}