namespace BirdFeed.Core.Extensions
{
  public static class StringExtensions
    {
        public static string ToApiString(this bool value)
        {
            return value ? "1" : "0";
        }
    }
}
