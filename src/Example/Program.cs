using System;
using ExcelNumberFormat;
using System.Globalization;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var format = new NumberFormat("[<=6e+3]# ??/\"a\"[Blue]?\"a\"0\"a\"");
            var result = format.Format(0.12, CultureInfo.InvariantCulture);
            Console.WriteLine("\"" + result.ToString() + "\"");
        }
    }
}
