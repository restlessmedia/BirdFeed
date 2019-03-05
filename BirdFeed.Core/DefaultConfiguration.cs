using BirdFeed.Core.Configuration;

namespace BirdFeed.Core
{
  public class DefaultConfiguration : IConfiguration
  {
    public DefaultConfiguration(IAuthCredentials authCredentials)
    {
      Provider = new DefaultAuthProvider(authCredentials);
    }

    public IAuthProvider Provider { get; private set; }
  }
}