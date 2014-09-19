using System.Configuration;

namespace BirdFeed.Core.Configuration
{
    public class Section : System.Configuration.ConfigurationSection, IConfiguration
    {
        [ConfigurationProperty(_authProperty, IsRequired = true)]
        public Element AuthElement
        {
            get
            {
                return this[_authProperty] as Element;
            }
            set
            {
                this[_authProperty] = value;
            }
        }

        public IAuthCredentials Auth
        {
            get { return AuthElement; }
        }

        private const string _authProperty = "auth";
    }
}