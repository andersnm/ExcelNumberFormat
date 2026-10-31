using System;

namespace ExcelNumberFormat
{
    /// <summary>
    /// An era of the Japanese calendar, used by the "g" and "e" date codes with the [$-411] locale.
    /// The table is built in, because JapaneseCalendar is not available on all target frameworks.
    /// </summary>
    internal class JapaneseEra
    {
        // Newest first
        private static readonly JapaneseEra[] Eras =
        {
            new JapaneseEra(new DateTime(2019, 5, 1), "令和", "令", "R"),
            new JapaneseEra(new DateTime(1989, 1, 8), "平成", "平", "H"),
            new JapaneseEra(new DateTime(1926, 12, 25), "昭和", "昭", "S"),
            new JapaneseEra(new DateTime(1912, 7, 30), "大正", "大", "T"),
            new JapaneseEra(new DateTime(1868, 1, 1), "明治", "明", "M"),
        };

        private JapaneseEra(DateTime start, string name, string shortName, string initial)
        {
            StartYear = start.Year;
            Start = start;
            Name = name;
            ShortName = shortName;
            Initial = initial;
        }

        public DateTime Start { get; }

        public int StartYear { get; }

        /// <summary>The full name, printed by "ggg".</summary>
        public string Name { get; }

        /// <summary>The first character of the name, printed by "gg".</summary>
        public string ShortName { get; }

        /// <summary>The Latin initial, printed by "g".</summary>
        public string Initial { get; }

        /// <summary>
        /// Returns the era of a date, or null for dates before the Meiji era.
        /// </summary>
        public static JapaneseEra Find(DateTime date)
        {
            foreach (var era in Eras)
            {
                if (date >= era.Start)
                    return era;
            }

            return null;
        }
    }
}