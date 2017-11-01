ExcelNumberFormat
=================

.NET library to parse ECMA-376 number format strings and format values like Excel and other spreadsheet softwares.

[![Build status](https://ci.appveyor.com/api/projects/status/pg23vtba9wjr138f?svg=true)](https://ci.appveyor.com/project/andersnm/excelnumberformat)

## Usage

```C#
var format = new NumberFormat("#.##");
Console.WriteLine(format.Format(1234.56, CultureInfo.InvariantCulture));
```

## Features

- Parses and formats most custom number formats as expected: decimal, percent, thousands, exponential, fraction, date/time, duration, text.
- Supports multiple sections with conditions.
- Formats values with relevant constants from CultureInfo.
- Formats dates and durations using DateTime and TimeSpan values instead of numeric values like Excel.
- Targets net20 and netstandard1.0 for max compatibility.

## TODO/notes

- 'General' is formatted with `.ToString()` instead of Excel conventions.
- No errors: Invalid format strings and incompatible input values are formatted with `.ToString()`.
- All modifiers in square brackets except conditions are ignored (f.ex color, currency).
- Variable width space is returned as regular space.
- Repeat-to-fill characters are printed once, not repeated.
- No alignment hinting.
