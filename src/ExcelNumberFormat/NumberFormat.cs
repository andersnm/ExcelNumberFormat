using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelNumberFormat
{
    public class NumberFormat
    {
        public List<Section> Sections = new List<Section>();

        public bool IsDateTimeFormat
        {
            get
            {
                return GetFirstSection(SectionType.Date) != null;
            }
        }

        public Section GetSection(object value)
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
