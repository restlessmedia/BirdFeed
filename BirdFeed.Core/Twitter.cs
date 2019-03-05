using BirdFeed.Core.Extensions;
using BirdFeed.Core.Request.Options;
using BirdFeed.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BirdFeed.Core
{
  internal class Twitter : ITwitter
  {
    public Twitter(IConfiguration configuration, ITwitterHttpClient client)
    {
      _configuration = configuration ?? throw new ArgumentNullException("configuration");
      _client = client ?? throw new ArgumentNullException("client");
    }

    public Tweet Latest(string username)
    {
      return Tweets(username, 1).FirstOrDefault();
    }

    public IEnumerable<Tweet> Tweets(int userId, int count)
    {
      return Tweets(new UserTimelineOptions(userId, count));
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

      const string uri = "https://api.twitter.com/1.1/statuses/user_timeline.json";

      IEnumerable<UserTimelineResponse> response = _client.Query<UserTimelineResponse>(uri, options, HttpMethod.Get);

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

      const string uri = "https://api.twitter.com/1.1/statuses/update.json";

      return _client.Post<UserTimelineResponse>(uri, options).ToTweet();
    }

    public Task<HttpResponseMessage> HandleCallbackAsync(HttpRequestMessage request)
    {
      return Task.Factory.StartNew(() => HandleCallback(request));
    }

    public HttpResponseMessage HandleCallback(HttpRequestMessage request)
    {
      RequestTokenRedirectResponse redirectResponse = new RequestTokenRedirectResponse(request.QueryToDictionary());
      IAccessToken accessToken = RequestAccessToken(redirectResponse);
      _configuration.Provider.Set(accessToken);
      return new HttpResponseMessage(HttpStatusCode.OK);
    }

    public HttpResponseMessage RequestTokenResponse(string callbackUri)
    {
      RequestTokenResponse tokenResponse = RequestToken(callbackUri);
      const string redirectFormat = "https://api.twitter.com/oauth/authenticate?oauth_token={0}";
      Uri redirect = new Uri(string.Format(redirectFormat, tokenResponse.oauth_token));
      HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Redirect);
      response.Headers.Location = redirect;
      return response;
    }
    
    private IAccessToken RequestAccessToken(RequestTokenRedirectResponse redirectResponse)
    {
      const string uri = "https://api.twitter.com/oauth/access_token";
      return _client.Post<RequestAccessTokenResponse>(uri, new RequestAccessTokenOptions(redirectResponse.oauth_verifier), response => new RequestAccessTokenResponse(response.ToKeyValueStringToDictionary()));
    }

    private RequestTokenResponse RequestToken(string callbackUri)
    {
      if (string.IsNullOrEmpty(callbackUri))
      {
        throw new ArgumentNullException("callbackUri");
      }

      const string uri = "https://api.twitter.com/oauth/request_token";

      return _client.Post<RequestTokenResponse>(uri, new RequestTokenOptions(callbackUri), response => new RequestTokenResponse(response.ToKeyValueStringToDictionary()));
    }

    private readonly IConfiguration _configuration;
    
    private readonly ITwitterHttpClient _client;
  }
}