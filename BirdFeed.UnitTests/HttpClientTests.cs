using BirdFeed.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BirdFeed.UnitTests
{
  [TestClass]
  public class HttpClientTests
  {
    public HttpClientTests()
    {
      _client = new HttpClient();
    }

    private readonly HttpClient _client;
  }
}