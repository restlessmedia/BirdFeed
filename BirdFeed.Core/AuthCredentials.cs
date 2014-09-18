namespace BirdFeed.Core
{
  public class AuthCredentials : IAuthCredentials
  {
    public AuthCredentials(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
    {
      ConsumerKey = consumerKey;
      ConsumerSecret = consumerSecret;
      AccessToken = accessToken;
      AccessTokenSecret = accessTokenSecret;
    }

    public string ConsumerKey { get; private set; }

    public string ConsumerSecret { get; private set; }

    public string AccessToken { get; private set; }

    public string AccessTokenSecret { get; private set; }
  }
}