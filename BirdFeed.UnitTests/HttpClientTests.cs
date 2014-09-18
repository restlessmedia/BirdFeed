using BirdFeed.Core;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BirdFeed.UnitTests
{
  [TestClass]
  public class HttpClientTests
  {
    public HttpClientTests()
    {
      _client = new HttpClient();
    }

    private readonly IAuthCredentials _auth;

    private readonly IHttpClient _httpClient;

    private readonly HttpClient _client;
  }
}