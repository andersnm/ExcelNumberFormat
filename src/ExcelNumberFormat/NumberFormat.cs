using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExcelNumberFormat
{
    public class NumberFormat
    {
        public NumberFormat(string formatString)
        {
            var tokenizer = new Tokenizer(formatString);
            var sections = new List<Section>();
            var isValid = true;
            while (true)
            {
                var section = Parser.ParseSection(tokenizer, out var syntaxError);

                if (syntaxError)
                    isValid = false;

                if (section == null)
                    break;

                sections.Add(section);
            }

            IsValid = isValid;
            FormatString = formatString;

            if (isValid)
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

        internal IReadOnlyList<Section> Sections { get; }

        public bool IsDateTimeFormat
        {
            get
            {
                return GetFirstSection(SectionType.Date) != null;
            }
        }

        public string Format(object value, CultureInfo culture)
        {
            if (!IsValid || string.IsNullOrEmpty(FormatString))
                return Convert.ToString(value, culture);

            var section = GetSection(value);
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

        internal Section GetSection(object value)
        {
            if (Sections.Count > 0)
                return Sections[0];
            return null;
            // TODO:
            // if datetime, return first datetime section, else null
            // if string, return first text section, else null
            // if double, int, check condition, order, negative etc, exponential, fraction
        }

        Section GetFirstSection(SectionType type)
        {
            foreach (var section in Sections)
                if (section.Type == type)
                    return section;
            return null;
        }
    }
}
