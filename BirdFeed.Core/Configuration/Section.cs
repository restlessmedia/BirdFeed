using System;
using System.Configuration;
using System.Reflection;

namespace BirdFeed.Core.Configuration
{
  public class Section : ConfigurationSection, IConfiguration
  {
    [ConfigurationProperty(_providerTypeProperty, IsRequired = true)]
    public string ProviderType
    {
      get
      {
        return (string)this[_providerTypeProperty];
      }
    }

    [ConfigurationProperty(_authProperty, IsRequired = false)]
    public AuthElement AuthElement
    {
      get
      {
        return (AuthElement)this[_authProperty];
      }
    }

    public IAuthCredentials Auth
    {
      get
      {
        return AuthElement;
      }
    }

    public IAuthProvider Provider
    {
      get
      {
        return New();
      }
    }

    public static Section GetSection(string name = "birdFeed")
    {
      Section section = ConfigurationManager.GetSection(name) as Section;

      if (section == null)
      {
        throw new ConfigurationErrorsException("Invalid configuration section");
      }

      return section;
    }

    private IAuthProvider New()
    {
      IAuthProvider provider = (IAuthProvider)GetConstructor().Invoke(null);
      return provider;
    }

    private ConstructorInfo GetConstructor()
    {
      ConstructorInfo ctor = GetProviderType().GetConstructor(Type.EmptyTypes);

      if (ctor == null)
      {
        throw new ConfigurationErrorsException(string.Format("No default constructor found for '{0}'", ProviderType));
      }

      return ctor;
    }

    private Type GetProviderType()
    {
      Type type = Type.GetType(ProviderType);

      if (type == null)
      {
        throw new ConfigurationErrorsException(string.Format("Unable to resolve provider type '{0}'", ProviderType));
      }

      return type;
    }

    private const string _providerTypeProperty = "provider";

    private const string _authProperty = "auth";
  }
}