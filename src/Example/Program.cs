using System;
using ExcelNumberFormat;
using System.Globalization;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Formatter.Format(0.12, "[<=6e+3]# ??/\"a\"[Blue]?\"a\"0\"a\"", CultureInfo.InvariantCulture);
            Console.WriteLine("\"" + result.ToString() + "\"");
        }
    }
}
