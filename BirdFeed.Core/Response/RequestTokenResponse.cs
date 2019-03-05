using System;
using System.Collections.Generic;

namespace BirdFeed.Core.Response
{
  internal class RequestTokenResponse
  {
    public RequestTokenResponse(IDictionary<string, string> formData)
    {
      if (formData == null)
      {
        throw new ArgumentNullException("formData");
      }

      SetFromDictionary(formData);
    }

    public string oauth_token { get; private set; }

    public string oauth_token_secret { get; private set; }

    public bool oauth_callback_confirmed { get; private set; }

    private void SetFromDictionary(IDictionary<string, string> data)
    {
      oauth_token = data["oauth_token"];
      oauth_token_secret = data["oauth_token_secret"];
      oauth_callback_confirmed = data["oauth_callback_confirmed"] == "true";
    }
  }
}