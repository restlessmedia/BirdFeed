using System.Collections.Generic;
using BirdFeed.Core.Request.Options;

namespace BirdFeed.Core
{
  public interface ITwitter
  {
    Tweet Latest(string username);

    IEnumerable<Tweet> Tweets(string username, int count);

    IEnumerable<Tweet> Tweets(UserTimelineOptions options);

    Tweet Tweet(string status);

    Tweet Tweet(UpdateStatusOptions options);
  }
}