using BirdFeed.Core;
using BirdFeed.Core.Request.Options;
using BirdFeed.Core.Response.Models;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace BirdFeed.UnitTests
{
  [TestClass]
  public class TwitterTests
  {
    public TwitterTests()
    {
      _client = A.Fake<ITwitterHttpClient>();
      _twitter = new Twitter(_client);
    }

    [TestMethod]
    public void Tweets_calls_client()
    {
      const string name = "screen_name";

      _twitter.Tweets(name, 10);

      A.CallTo(() => _client.Query<UserTimelineOptions, UserTimelineResponse>(A<string>.Ignored, A<UserTimelineOptions>.Ignored, HttpMethod.Get)).MustHaveHappened();
    }

    private readonly ITwitterHttpClient _client;

    private readonly Twitter _twitter;
  }
}