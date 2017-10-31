using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            return Formatter.Format(value, this, culture);
        }

        internal Section GetSection(object value)
        {
            if (Sections.Any())
                return Sections[0];
            return null;
            // TODO:
            // if datetime, return first datetime section, else null
            // if string, return first text section, else null
            // if double, int, check condition, order, negative etc, exponential, fraction
        }

        private Section GetFirstSection(SectionType type)
        {
            return Sections.FirstOrDefault(s => s.Type == type);
        }
    }
}
