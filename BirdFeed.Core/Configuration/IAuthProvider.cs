namespace BirdFeed.Core.Configuration
{
  public interface IAuthProvider
  {
    IAuthCredentials Get();

    void Set(IAccessToken accessToken);
  }
}