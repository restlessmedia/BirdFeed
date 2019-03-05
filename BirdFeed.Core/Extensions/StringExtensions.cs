using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BirdFeed.Core.Extensions
{
  public static class StringExtensions
  {
    public static string ToApiBoolean(this bool value)
    {
      const string trueValue = "1";
      const string falseValue = "0";

      return value ? trueValue : falseValue;
    }

    public static string FormatUrls(this string input, string format = "<a href=\"{0}\">{0}</a>")
    {
      if (string.IsNullOrEmpty(input))
      {
        return input;
      }

      const string pattern = "http[s]?://[^\\s]+";

      return Format(input, pattern, format);
    }

    public static string Format(this string input, string pattern, string format)
    {
      if (string.IsNullOrEmpty(input))
      {
        return input;
      }

      return Regex.Replace(input, pattern, x => string.Format(format, x.Value));
    }

    public static IDictionary<string, string> ToKeyValueStringToDictionary(this string input, char separator = '&', char keyValueSeparator = '=')
    {
      if (string.IsNullOrEmpty(input))
      {
        return new Dictionary<string, string>();
      }

      return input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(keyValueSeparator)).ToDictionary(x => x[0], x => x.Length > 1 ? x[1] : null);
    }
  }
}