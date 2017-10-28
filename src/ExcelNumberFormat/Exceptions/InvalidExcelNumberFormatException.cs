using System;

namespace ExcelNumberFormat.Exceptions
{
    public class InvalidExcelNumberFormatException : Exception
    {
        public InvalidExcelNumberFormatException()
        { }

        public InvalidExcelNumberFormatException(string message)
            : this(message, null)
        { }

        public InvalidExcelNumberFormatException(string message, Exception innerException)
        : base(message, innerException)
        { }
    }
}