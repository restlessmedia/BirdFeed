namespace BirdFeed.Core
{
  public interface IAccessToken
  {
    string AccessToken { get; set; }

    string AccessTokenSecret { get; set; }
  }
}