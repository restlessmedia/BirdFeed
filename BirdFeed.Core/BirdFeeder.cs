using BirdFeed.Core.Configuration;
using System.Configuration;

namespace BirdFeed.Core
{
    public static class BirdFeeder
    {
        public static ITwitter Create(IAuthCredentials auth)
        {
            return new Twitter(CreateClient(auth));
        }

        public static ITwitter Create()
        {
            return Create(Configuration().Auth);
        }

        private static IConfiguration Configuration()
        {
            Section section = ConfigurationManager.GetSection("birdFeed") as Section;

            if(section == null)
      {
        throw new ConfigurationErrorsException("Invalid configuration section");
      }

      return section;
        }

        private static ITwitterHttpClient CreateClient()
        {
            return CreateClient(Configuration().Auth);
        }

        private static ITwitterHttpClient CreateClient(IAuthCredentials auth)
        {
            return new TwitterHttpClient(new HttpClient(), auth);
        }
    }
}