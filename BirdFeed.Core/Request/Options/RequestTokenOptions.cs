using System.Collections.Generic;

namespace BirdFeed.Core.Request.Options
{
  public class RequestTokenOptions : IApiOptions
  {
    public RequestTokenOptions(string callbackUri)
    {
      CallbackUri = callbackUri;
    }

    public string CallbackUri { get; private set; }

    public IDictionary<string, string> Parameters
    {
      get
      {
        return new Dictionary<string, string>
        {
          { "oauth_callback", CallbackUri}
        };
      }
    }
  }
}