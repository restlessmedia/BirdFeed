using System;

namespace BirdFeed.Core.Extensions
{
  public static class DateTimeExtensions
  {
    public static int SecondsSinceEpoch(this DateTime dateTime)
    {
      return (int)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public static int SecondsSinceEpoch()
    {
      return SecondsSinceEpoch(DateTime.UtcNow);
    }
  }
}