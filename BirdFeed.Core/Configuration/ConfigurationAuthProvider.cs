namespace BirdFeed.Core.Configuration
{
  public abstract class ConfigurationAuthProvider : IAuthProvider
  {
    public IAuthCredentials Get()
    {
      return Section.GetSection().Auth;
    }

    public abstract void Set(IAccessToken accessToken);
  }
}