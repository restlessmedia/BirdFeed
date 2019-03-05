using BirdFeed.Core.Request.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BirdFeed.Core
{
  public interface ITwitter
  {
    Tweet Latest(string username);

    IEnumerable<Tweet> Tweets(int userId, int count);

    IEnumerable<Tweet> Tweets(string username, int count);

    IEnumerable<Tweet> Tweets(UserTimelineOptions options);

    Tweet Tweet(string status);

    Tweet Tweet(UpdateStatusOptions options);

    Task<HttpResponseMessage> HandleCallbackAsync(HttpRequestMessage request);

    HttpResponseMessage RequestTokenResponse(string callbackUri);
  }
}