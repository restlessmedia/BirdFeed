using System;
using System.Collections.Generic;
using System.Linq;
using BirdFeed.Core.Extensions;
using BirdFeed.Core.Request.Options;
using BirdFeed.Core.Response.Models;
using System.Net.Http;

namespace BirdFeed.Core
{
  internal class Twitter : ITwitter
  {
    public Twitter(ITwitterHttpClient client)
    {
      _client = client ?? throw new ArgumentNullException("client");
    }

    public Tweet Latest(string username)
    {
      return Tweets(username, 1).FirstOrDefault();
    }

    public IEnumerable<Tweet> Tweets(string username, int count)
    {
      if (string.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException("username");
      }

      return Tweets(new UserTimelineOptions(username, count));
    }

    public IEnumerable<Tweet> Tweets(UserTimelineOptions options)
    {
      if (options == null)
      {
        throw new ArgumentNullException("options");
      }

      IEnumerable<UserTimelineResponse> response = _client.Query<UserTimelineOptions, UserTimelineResponse>("https://api.twitter.com/1.1/statuses/user_timeline.json", options, HttpMethod.Get);

      if (response == null)
      {
        return Enumerable.Empty<Tweet>();
      }

      return response.Select(x => x.ToTweet());
    }

    public Tweet Tweet(string status)
    {
      if (string.IsNullOrEmpty(status))
      {
        throw new ArgumentNullException("status");
      }

      return Tweet(new UpdateStatusOptions(status));
    }

    public Tweet Tweet(UpdateStatusOptions options)
    {
      if (options == null)
      {
        throw new ArgumentNullException("options");
      }

      return _client.Post<UpdateStatusOptions, UserTimelineResponse>("https://api.twitter.com/1.1/statuses/update.json", options).ToTweet();
    }

    private readonly ITwitterHttpClient _client;
  }
}