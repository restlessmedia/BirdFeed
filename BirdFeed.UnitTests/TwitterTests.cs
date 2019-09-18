using BirdFeed.Core;
using BirdFeed.Core.Request;
using BirdFeed.Core.Request.Options;
using BirdFeed.Core.Response;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;

namespace BirdFeed.UnitTests
{
  [TestClass]
  public class TwitterTests
  {
    public TwitterTests()
    {
      _client = A.Fake<ITwitterHttpClient>();
      _twitter = new Twitter(A.Fake<IConfiguration>(), _client);
    }

    [TestMethod]
    public void Tweets_calls_client_with_name()
    {
      const string name = "screen_name";
      const int count = 10;
      const string uri = "https://api.twitter.com/1.1/statuses/user_timeline.json";

      A.CallTo(() => _client.Query<UserTimelineResponse>(A<string>.Ignored, A<UserTimelineOptions>.Ignored, HttpMethod.Get)).Returns(Enumerable.Empty<UserTimelineResponse>());

      _twitter.Tweets(name, count);

      AssertQuery<UserTimelineOptions, UserTimelineResponse>(uri, x => x.Parameters["screen_name"].Equals(name) && x.Parameters["count"].Equals(count.ToString()), HttpMethod.Get);
    }

    [TestMethod]
    public void Tweets_calls_client_with_id()
    {
      const int id = 999;
      const int count = 10;
      const string uri = "https://api.twitter.com/1.1/statuses/user_timeline.json";

      A.CallTo(() => _client.Query<UserTimelineResponse>(A<string>.Ignored, A<UserTimelineOptions>.Ignored, HttpMethod.Get)).Returns(Enumerable.Empty<UserTimelineResponse>());

      _twitter.Tweets(id, count);

      AssertQuery<UserTimelineOptions, UserTimelineResponse>(uri, x => x.Parameters["user_id"].Equals(id.ToString()) && x.Parameters["count"].Equals(count.ToString()), HttpMethod.Get);
    }

    [TestMethod]
    public void Tweet_calls_client()
    {
      const string status = "something, something, darkside";

      A.CallTo(() => _client.Query<UserTimelineResponse>(A<string>.Ignored, A<UserTimelineOptions>.Ignored, HttpMethod.Get)).Returns(Enumerable.Empty<UserTimelineResponse>());

      _twitter.Tweet(status);
    }

    [TestMethod]
    public void asd()
    {
      IAuthCredentials authCredentials = new AuthCredentials("4qCE4TdIxBmIBg4V74vAw", "AfDVun8eRxJngbPvL5CWz9hSet0u7j7iWpz1dVqOmg", "378507093-07LQVYYXwCA5kQEN68Do6oatBDobNDIhvRExPlfJ", "XEfZmzAr4rtYc5yeTZaqBR9iE8aEUnvpt5I7MDpF8");
      IConfiguration configuration = new DefaultConfiguration(authCredentials);

      IHttpClient httpClient = new Core.HttpClient();
      ITwitterHttpClient twitterHttpClient = new TwitterHttpClient(httpClient, authCredentials);
      Twitter twitter = new Twitter(configuration, twitterHttpClient);

      twitter.Latest("@blakestanleye8");
    }

    private void AssertQuery<TOptions, TResult>(string uri, Func<TOptions, bool> optionValidate, HttpMethod method)
      where TOptions : IApiOptions
    {
      A.CallTo(() => _client.Query<TResult>(A<string>.Ignored, A<TOptions>.Ignored, A<HttpMethod>.Ignored)).WhenArgumentsMatch(args => args.Get<string>(0).Equals(uri) && optionValidate(args.Get<TOptions>(1)) && args.Get<HttpMethod>(2).Equals(method)).MustHaveHappened();
    }

    private readonly ITwitterHttpClient _client;

    private readonly Twitter _twitter;
  }
}