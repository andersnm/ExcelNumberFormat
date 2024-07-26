using System.Collections.Generic;

namespace ExcelNumberFormat
{
    /// <summary>
    /// Represents a Windows Language Code Identifier (LCID) as defined by [MS-LCID] (https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid/70feba9f-294e-491e-b6eb-56532684c37f)
    /// </summary>
    internal class WindowsLanguageCodeIdentifier
    {
        public int LocaleId { get; set; }

        public bool IsLongSystemDate => LocaleId == 0xF800;

        public bool IsLongSystemTime => LocaleId == 0xF400;

        /// <summary>
        /// If IsLongSystemTime is true, then contains the tokens for the time formatter.
        /// </summary>
        public List<string> TimeTokens { get; set; }
    }
}
