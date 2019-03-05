using BirdFeed.Core.Response;
using System;
using System.Globalization;

namespace BirdFeed.Core.Extensions
{
  internal static class ModelExtensions
  {
    public static Tweet ToTweet(this UserTimelineResponse response)
    {
      return new Tweet
      {
        Text = response.text,
        Created = response.created_at.ParseDate()
      };
    }

    public static DateTime ParseDate(this string value)
    {
      const string format = "ddd MMM dd HH:mm:ss zzz yyyy";
      DateTime result;
      return DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result) ? result : DateTime.MinValue;
    }
  }
}