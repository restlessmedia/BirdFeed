using System;
using System.Collections.Generic;

namespace BirdFeed.Core.Response
{
  public class RequestAccessTokenResponse : IAccessToken
  {
    public RequestAccessTokenResponse(IDictionary<string, string> query)
    {
      if (query == null)
      {
        throw new ArgumentNullException("query");
      }

      SetFromDictionary(query);
    }

    public string AccessToken { get; set; }

    public string AccessTokenSecret { get; set; }

    private void SetFromDictionary(IDictionary<string, string> data)
    {
      AccessToken = data["oauth_token"];
      AccessTokenSecret = data["oauth_token_secret"];
    }
  }
}