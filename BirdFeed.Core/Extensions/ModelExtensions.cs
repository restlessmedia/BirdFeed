using BirdFeed.Core.Response.Models;

namespace BirdFeed.Core.Extensions
{
    internal static class ModelExtensions
    {
        public static Tweet ToTweet(this UserTimelineResponse response)
        {
            return new Tweet
            {
                Text = response.text
            };
        }
    }
}
