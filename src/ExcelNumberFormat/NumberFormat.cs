using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExcelNumberFormat
{
    public class NumberFormat
    {
        public NumberFormat(string formatString)
        {
            var sections = Parser.ParseSections(formatString, out bool syntaxError);

            IsValid = !syntaxError;
            FormatString = formatString;

            if (IsValid)
            {
                Sections = sections;
            }
            else
            {
                Sections = new List<Section>();
            }
        }

        public bool IsValid { get; }

        public string FormatString { get; }

        internal List<Section> Sections { get; }

        public bool IsDateTimeFormat
        {
            get
            {
                return Evaluator.GetFirstSection(Sections, SectionType.Date) != null;
            }
        }

        public bool IsTimeSpanFormat
        {
            get
            {
                return Evaluator.GetFirstSection(Sections, SectionType.Duration) != null;
            }
        }

        public string Format(object value, CultureInfo culture)
        {
            if (!IsValid || string.IsNullOrEmpty(FormatString))
                return Convert.ToString(value, culture);

            var section = Evaluator.GetSection(Sections, value);
            if (section == null)
                return Convert.ToString(value, culture);

            try
            {
                return Formatter.Format(value, section, culture);
            }
            catch (InvalidCastException)
            {
                // TimeSpan cast exception
                return Convert.ToString(value, culture);
            }
            catch (FormatException)
            {
                // Convert.ToDouble/ToDateTime exceptions
                return Convert.ToString(value, culture);
            }
        }
    }
}
