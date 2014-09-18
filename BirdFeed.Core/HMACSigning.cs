using System;
using System.Security.Cryptography;
using System.Text;

namespace BirdFeed.Core
{
  public class HMACSigning : IOAuthSigning
  {
    public string Sign(string key, string value)
    {
      if (string.IsNullOrEmpty(key))
        throw new ArgumentNullException("key");

      if (string.IsNullOrEmpty(value))
        throw new ArgumentNullException("value");

      ASCIIEncoding encoding = new ASCIIEncoding();
      HMACSHA1 hash = new HMACSHA1(encoding.GetBytes(key));
      return Convert.ToBase64String(hash.ComputeHash(encoding.GetBytes(value)));
    }

    public string Method
    {
      get { return _name; }
    }

    private const string _name = "HMAC-SHA1";
  }
}
