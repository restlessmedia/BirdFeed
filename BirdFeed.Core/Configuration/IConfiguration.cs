using BirdFeed.Core.Configuration;

namespace BirdFeed.Core
{
  public interface IConfiguration
  {
    IAuthProvider Provider { get; }
  }
}