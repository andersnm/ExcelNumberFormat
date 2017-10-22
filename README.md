ExcelNumberFormat
=================

ExcelNumberFormat is an experimental .NET library to parse and render ECMA-376 number format strings used by Excel and other spreadsheet softwares.

The project started as a fun and challenging exercise, like solving a puzzle. It is not yet a complete or usable implementation. 

```C#
Console.WriteLine(Formatter.Format(1234.56, "#.##", CultureInfo.InvariantCulture));
```

Features:
- Parses and renders most custom formats as expected: decimal, percent, thousands, exponential, fraction, date/time, text
- Parses most legal format strings
- Renders output using relevant constants from CultureInfo
- Dates are formatted using a .NET DateTime value (instead of a numeric value like Excel)

TODO:
- 'General' format is parsed, but not formatted correctly
- Conditions and colors are parsed, but not used. Other modifiers in square brackets are skipped
- Multi-section formats are parsed, but only the first section is used for formatting
- And probably a lot more!
