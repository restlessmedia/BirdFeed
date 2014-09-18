using BirdFeed.Core;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BirdFeed.UnitTests
{
  [TestClass]
  public class TwitterHttpClientTests
  {
    public TwitterHttpClientTests()
    {
      _auth = A.Fake<IAuthCredentials>();
      _httpClient = A.Fake<IHttpClient>();
      _client = new TwitterHttpClient(_httpClient, _auth);
    }

    private readonly IAuthCredentials _auth;

    private readonly IHttpClient _httpClient;

    private readonly TwitterHttpClient _client;
  }
}