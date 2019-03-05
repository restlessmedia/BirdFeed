using System.Configuration;

namespace BirdFeed.Core.Configuration
{
  public class AuthElement : System.Configuration.ConfigurationElement, IAuthCredentials
  {
    [ConfigurationProperty(_consumerKeyProperty, IsRequired = true)]
    public string ConsumerKey
    {
      get
      {
        return this[_consumerKeyProperty] as string;
      }
      set
      {
        this[_consumerKeyProperty] = value;
      }
    }

    [ConfigurationProperty(_consumerSecretProperty, IsRequired = true)]
    public string ConsumerSecret
    {
      get
      {
        return this[_consumerSecretProperty] as string;
      }
      set
      {
        this[_consumerSecretProperty] = value;
      }
    }

    [ConfigurationProperty(_accessTokenProperty, IsRequired = true)]
    public string AccessToken
    {
      get
      {
        return this[_accessTokenProperty] as string;
      }
      set
      {
        this[_accessTokenProperty] = value;
      }
    }

    [ConfigurationProperty(_accessTokenSecretProperty, IsRequired = true)]
    public string AccessTokenSecret
    {
      get
      {
        return this[_accessTokenSecretProperty] as string;
      }
      set
      {
        this[_accessTokenSecretProperty] = value;
      }
    }

    private const string _consumerKeyProperty = "consumerKey";

    private const string _consumerSecretProperty = "consumerSecret";

    private const string _accessTokenProperty = "accessToken";

    private const string _accessTokenSecretProperty = "accessTokenSecret";
  }
}