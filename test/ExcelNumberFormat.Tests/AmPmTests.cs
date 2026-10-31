using System;
using System.Globalization;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

// The expected values were produced by Excel 16 with the same format strings.

namespace ExcelNumberFormat.Tests
{
    [TestClass]
    public class AmPmTests
    {
        private static readonly DateTime Morning = new DateTime(2020, 1, 1, 6, 0, 0);
        private static readonly DateTime Evening = new DateTime(2020, 1, 1, 18, 0, 0);

        string Format(object value, string formatString, CultureInfo culture)
        {
            var format = new NumberFormat(formatString);
            if (format.IsValid)
                return format.Format(value, culture);

            return null;
        }

        void Test(object value, string format, string expected)
        {
            var result = Format(value, format, CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestLocaleCodeSelectsDesignators()
        {
            // https://github.com/andersnm/ExcelNumberFormat/issues/45
            Test(Morning, "[$-405]h:mm AM/PM", "6:00 dop.");
            Test(Evening, "[$-405]h:mm AM/PM", "6:00 odp.");
            Test(Morning, "[$-405]h:mm am/pm", "6:00 dop.");
            Test(Morning, "[$-411]h:mm AM/PM", "6:00 午前");
            Test(Evening, "[$-411]h:mm AM/PM", "6:00 午後");
            Test(Morning, "[$-804]h:mm AM/PM", "6:00 上午");
            Test(Evening, "[$-804]h:mm AM/PM", "6:00 下午");
            Test(Evening, "[$-409]h:mm AM/PM", "6:00 PM");
        }

        [TestMethod]
        public void TestLocaleCodeAlsoAppliesToShortDesignator()
        {
            // Excel only keeps the letters of A/P when the format has no locale code
            Test(Morning, "[$-405]h:mm A/P", "6:00 dop.");
            Test(Evening, "[$-405]h:mm a/p", "6:00 odp.");
            Test(Evening, "[$-409]h:mm A/P", "6:00 PM");
            Test(Morning, "[$-407]h:mm a/p", "6:00 AM");
        }

        [TestMethod]
        public void TestCultureSelectsDesignatorsWithoutLocaleCode()
        {
            var cs = new CultureInfo("cs-CZ");
            Assert.AreEqual("6:00 " + cs.DateTimeFormat.AMDesignator, Format(Morning, "h:mm AM/PM", cs));
            Assert.AreEqual("6:00 " + cs.DateTimeFormat.PMDesignator, Format(Evening, "h:mm AM/PM", cs));
            Assert.AreEqual("6:00 P", Format(Evening, "h:mm A/P", cs));
            Assert.AreEqual("6:00 p", Format(Evening, "h:mm a/p", cs));
        }

        [TestMethod]
        public void TestLocaleCodeWinsOverCulture()
        {
            var cs = new CultureInfo("cs-CZ");
            Assert.AreEqual("6:00 PM", Format(Evening, "[$-409]h:mm AM/PM", cs));
            Assert.AreEqual("6:00 PM", Format(Evening, "[$-1010409]h:mm AM/PM", cs));
        }
    }
}