using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BirdFeed.Core
{
  public class CallbackMessageHandler : HttpMessageHandler
  {
    public CallbackMessageHandler(ITwitter twitter)
    {
      _twitter = twitter ?? throw new ArgumentNullException(nameof(twitter));
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      return _twitter.HandleCallbackAsync(request);
    }

    private readonly ITwitter _twitter;
  }
}