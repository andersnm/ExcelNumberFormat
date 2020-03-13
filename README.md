ExcelNumberFormat
=================

.NET library to parse ECMA-376 number format strings and format values like Excel and other spreadsheet softwares.

[![Build status](https://ci.appveyor.com/api/projects/status/pg23vtba9wjr138f?svg=true)](https://ci.appveyor.com/project/andersnm/excelnumberformat)

## Install via NuGet

If you want to include ExcelNumberFormat in your project, you can [install it directly from NuGet](https://www.nuget.org/packages/ExcelNumberFormat)

To install ExcelNumberFormat, run the following command in the Package Manager Console

```
PM> Install-Package ExcelNumberFormat
```

## Usage

```C#
var format = new NumberFormat("#.##");
Console.WriteLine(format.Format(1234.56, CultureInfo.InvariantCulture));
```

## Features

- Parses and formats most custom number formats as expected: decimal, percent, thousands, exponential, fraction, currency, date/time, duration, text.
- Supports multiple sections with conditions.
- Formats values with relevant constants from CultureInfo.
- Formats dates and durations using DateTime and TimeSpan values instead of numeric values like Excel.
- Targets net20 and netstandard1.0 for max compatibility.

## Formatting .NET types

The `Format()` method takes a value of type `object` as parameter. Internally, the value is cast or converted to a specific .NET type depending on the kind of number format:

Format Kind | Example | .NET type|Conversion strategy
-|-|-|-
Number   | 0.00      |double|Convert.ToDouble()
Fraction | 0/0       |double|Convert.ToDouble()
Exponent | \#0.0E+0  |double|Convert.ToDouble()
Date/Time| hh\:mm    |DateTime|Convert.ToDateTime()
Duration | \[hh\]\:mm|TimeSpan|Cast to TimeSpan
General  | General   |(any)|CompatibleConvert.ToString()
Text     | ;;;"Text: "@|string|Convert.ToString()

In case of errors, `Format()` returns the value from `CompatibleConvert.ToString()`.

`CompatibleConvert.ToString()` formats floats and doubles with explicit precision, or falls back to `Convert.ToString()` for any other types.

## TODO/notes

- 'General' is formatted with `.ToString()` instead of Excel conventions.
- No errors: Invalid format strings and incompatible input values are formatted with `.ToString()`.
- No color information.
- Variable width space is returned as regular space.
- Repeat-to-fill characters are printed once, not repeated.
- No alignment hinting.
- No date conditions.
