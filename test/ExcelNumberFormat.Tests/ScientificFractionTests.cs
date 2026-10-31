using System.Globalization;
using NUnit.Framework;

namespace ExcelNumberFormat.Tests
{
    [TestFixture]
    public class ScientificFractionTests
    {
        void Test(object value, string formatString, string expected)
        {
            var format = new NumberFormat(formatString);
            Assert.IsTrue(format.IsValid);
            Assert.AreEqual(expected, format.Format(value, CultureInfo.InvariantCulture));
        }

        [Test]
        public void TestExponentZeroAndCarry()
        {
            Test(0, "0.00E+00", "0.00E+00");
            Test(9.999, "0.00E+00", "1.00E+01");
            Test(99999, "0.0E+0", "1.0E+5");
            Test(999.99, "##0.0E+0", "1.0E+3");
            Test(0.00099999, "0.00E+00", "1.00E-03");
            Test(-9.999, "0.00E+00", "-1.00E+01");
        }

        [Test]
        public void TestExponentSignInSections()
        {
            Test(-5, "0.0E+00", "-5.0E+00");
            Test(-5, "0.0E+00;0.0E+00", "5.0E+00");
            Test(-5, "0.0E+00;-0.0E+00", "-5.0E+00");
        }

        [Test]
        public void TestFractionConstantDenominator()
        {
            Test(-0.25, "?/4", "-1/4");
            Test(1.75, "?/4", "7/4");
            Test(1.75, "# ?/4", "1 3/4");
            Test(-1.75, "# ?/4", "-1 3/4");
        }

        [Test]
        public void TestFractionRoundingCarry()
        {
            Test(2.99, "# ?/4", "3    ");
            Test(2.9999999, "# ?/?", "3    ");
            Test(0.9999999, "?/?", "1/1");
        }

        [Test]
        public void TestFractionSignInSections()
        {
            Test(-0.5, "?/?", "-1/2");
            Test(-0.5, "?/?;?/?", "1/2");
            Test(-2.5, "# ?/?", "-2 1/2");
            Test(-2.5, "# ?/?;# ?/?", "2 1/2");
        }

        [Test]
        public void TestFractionLargeValues()
        {
            Test((double)int.MaxValue, "# ?/?", int.MaxValue + "    ");
            Test((double)int.MaxValue + 1, "# ?/?", ((long)int.MaxValue + 1) + "    ");
            Test(3e9, "# ?/?", "3000000000    ");
        }
    }
}
