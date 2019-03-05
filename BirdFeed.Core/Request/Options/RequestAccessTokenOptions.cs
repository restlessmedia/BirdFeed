using System.Collections.Generic;

namespace BirdFeed.Core.Request.Options
{
  public class RequestAccessTokenOptions : IApiOptions
  {
    public RequestAccessTokenOptions(string oAuthVerifier)
    {
      oauth_verifier = oAuthVerifier;
    }

    public string x_auth_password { get; set; }

    public string x_auth_username { get; set; }

    public string x_auth_mode { get; set; }

    public string oauth_verifier { get; set; }

    public IDictionary<string, string> Parameters
    {
      get
      {
        IDictionary<string, string> parameters = new Dictionary<string, string>
        { 
          { "oauth_verifier", oauth_verifier}
        };

        if (!string.IsNullOrEmpty(x_auth_password))
        {
          parameters.Add("x_auth_password", x_auth_password);
        }

        if (!string.IsNullOrEmpty(x_auth_username))
        {
          parameters.Add("x_auth_username", x_auth_password);
        }

        if (!string.IsNullOrEmpty(x_auth_mode))
        {
          parameters.Add("x_auth_mode", x_auth_mode);
        }

        return parameters;
      }
    }
  }
}