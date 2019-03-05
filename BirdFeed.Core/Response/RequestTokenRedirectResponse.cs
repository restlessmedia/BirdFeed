using System;
using System.Collections.Generic;

namespace BirdFeed.Core.Response
{
  public class RequestTokenRedirectResponse
  {
    public RequestTokenRedirectResponse(IDictionary<string, string> query)
    {
      if (query == null)
      {
        throw new ArgumentNullException("query");
      }

      SetFromDictionary(query);
    }

    public string oauth_token { get; private set; }

    public string oauth_verifier { get; private set; }

    private void SetFromDictionary(IDictionary<string, string> data)
    {
      oauth_token = data["oauth_token"];
      oauth_verifier = data["oauth_verifier"];
    }
  }
}