namespace BirdFeed.Core
{
  public interface IAuthCredentials
  {
    string ConsumerKey { get; }

    string ConsumerSecret { get; }

    string AccessToken { get; }

    string AccessTokenSecret { get; }
  }
}