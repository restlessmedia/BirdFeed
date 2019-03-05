using BirdFeed.Core.Configuration;
using System.Web.Http;

namespace BirdFeed.Core
{
  public static class BirdFeeder
  {
    public static ITwitter Create(IConfiguration configuration)
    {
      TwitterHttpClient client = new TwitterHttpClient(new HttpClient(), configuration.Provider.Get());
      return new Twitter(configuration, client);
    }
    
    public static ITwitter Create()
    {
      return Create(Section.GetSection());
    }

    public static ITwitter Create(IAuthCredentials authCredentials)
    {
      return Create(new DefaultConfiguration(authCredentials));
    }

    public static void RegisterCallback(ITwitter twitter, HttpRouteCollection routes, string routeName)
    {
      routes.MapHttpRoute("BirdFeedTwitterCallback", routeName, null, null, new CallbackMessageHandler(twitter));
    }
  }
}