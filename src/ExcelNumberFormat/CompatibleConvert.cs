using System;
using System.Globalization;

namespace ExcelNumberFormat
{
    internal static class CompatibleConvert
    {
        /// <summary>
        /// Maps the other numeric types onto double. Other values are returned unchanged.
        /// </summary>
        public static object Normalize(object value)
        {
            switch (value)
            {
                case byte _:
                case sbyte _:
                case ushort _:
                case uint _:
                case long _:
                case ulong _:
                case decimal _:
                    return Convert.ToDouble(value, CultureInfo.InvariantCulture);
                case float f:
                    // Whole numbers that print as integers widen exactly, 123456792f stays 123456792.
                    if (float.IsNaN(f) || float.IsInfinity(f) || (f == Math.Floor(f) && Math.Abs(f) < 1e15f))
                        return (double)f;

                    // Others go through a short string, so 1234.56f becomes 1234.56 and not 1234.56005859375.
                    // Very large floats keep 7 significant digits, as General does.
                    var format = f == Math.Floor(f) ? "G7" : "R";
                    return double.Parse(f.ToString(format, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                default:
                    return value;
            }
        }

        /// <summary>
        /// A backward-compatible version of <see cref="Convert.ToString(object, IFormatProvider)"/>.
        /// Starting from .net Core 3.0 the default precision used for formatting floating point number has changed.
        /// To always format numbers the same way, no matter what version of runtime is used, we specify the precision explicitly.
        /// </summary>
        public static string ToString(object value, IFormatProvider provider)
        {
            switch (value)
            {
                case double d:
                    return d.ToString("G15", provider);
                case float f:
                    return f.ToString("G7", provider);
                default:
                    return Convert.ToString(value, provider);
            }
        }
    }
}