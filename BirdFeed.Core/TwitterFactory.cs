namespace BirdFeed.Core
{
  public static class TwitterFactory
  {
    public static ITwitter Create(IAuthCredentials auth)
    {
      return new Twitter(CreateClient(auth));
    }

    public static ITwitter Create()
    {
      return Create(DefaultConfiguration());
    }

    public static IConfiguration DefaultConfiguration()
    {
      return new DefaultConfiguration();
    }

    private static ITwitterHttpClient CreateClient()
    {
      return CreateClient(DefaultConfiguration());
    }

    private static ITwitterHttpClient CreateClient(IAuthCredentials auth)
    {
      return new TwitterHttpClient(new HttpClient(), auth);
    }
  }
}