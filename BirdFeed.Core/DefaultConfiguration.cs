using System.Configuration;
namespace BirdFeed.Core
{
  public class DefaultConfiguration : IConfiguration
  {
    public string ConsumerKey
    {
      get { return GetAppSetting("ConsumerKey"); }
    }

    public string ConsumerSecret
    {
      get { return GetAppSetting("ConsumerSecret"); }
    }

    public string AccessToken
    {
      get { return GetAppSetting("AccessToken"); }
    }

    public string AccessTokenSecret
    {
      get { return GetAppSetting("AccessTokenSecret"); }
    }

    private string GetAppSetting(string name)
    {
      return ConfigurationManager.AppSettings[GetAppSettingKey(name)];
    }

    private string GetAppSettingKey(string name)
    {
      return string.Concat("twitter:", name);
    }
  }
}