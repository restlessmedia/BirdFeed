using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using BirdFeed.Core.Extensions;

namespace BirdFeed.Core
{
  internal class OAuth
  {
    public OAuth(IOAuthSigning signing, IAuthCredentials auth, HttpMethod method, string baseUri, IDictionary<string, string> requestParameters, string nonce, string timestamp)
    {
      if (string.IsNullOrEmpty(baseUri))
      {
        throw new ArgumentNullException("baseUri");
      }

      if (requestParameters == null)
      {
        throw new ArgumentNullException("requestParameters");
      }

      if (requestParameters.Count == 0)
      {
        throw new ArgumentException("Invalid number of request parameters", "requestParameters");
      }

      if (string.IsNullOrEmpty(nonce))
      {
        throw new ArgumentNullException("baseUri");
      }

      if (string.IsNullOrEmpty(timestamp))
      {
        throw new ArgumentNullException("timestamp");
      }

      _signing = signing ?? throw new ArgumentNullException("signing");
      _auth = auth ?? throw new ArgumentNullException("auth");
      _method = method;
      _baseUri = baseUri;
      _requestParameters = requestParameters;
      _nonce = nonce;
      _timestamp = timestamp;
    }

    public OAuth(IAuthCredentials auth, HttpMethod method, string baseUri, IDictionary<string, string> requestParameters, string nonce, string timestamp)
      : this(new HMACSigning(), auth, method, baseUri, requestParameters, nonce, timestamp) { }

    public OAuth(IAuthCredentials auth, HttpMethod method, string baseUri, IDictionary<string, string> requestParameters)
      : this(auth, method, baseUri, requestParameters, NewNonce(), NewTimestamp()) { }

    public override string ToString()
    {
      return Signature();
    }

    public void SetAuthorisationHeader(NameValueCollection headers)
    {
      headers["Authorization"] = AuthHeader();
    }

    public string Signature()
    {
      return _signing.Sign(SigningKey(), SignatureBaseString());
    }

    public string AuthHeader()
    {
      return string.Concat("OAuth ", AuthHeaderValue());
    }

    public string AuthHeaderValue()
    {
      const string format = "{0}=\"{1}\"";
      const string separator = ", ";
      return string.Join(separator, GetOAuthParameters(Signature()).Select(x => string.Format(format, PercentEncode(x.Key), PercentEncode(x.Value))));
    }

    public string SignatureBaseString()
    {
      const string format = "{0}&{1}&{2}";
      return string.Format(format, _method, PercentEncode(_baseUri.ToString()), PercentEncode(ParameterString()));
    }

    public string ParameterString()
    {
      const string separator = "&";
      const string format = "{0}={1}";
      return String.Join(separator, GetOrderedSigningParameters().Select(x => string.Format(format, PercentEncode(x.Key), PercentEncode(x.Value))));
    }

    public string SigningKey()
    {
      const string format = "{0}&{1}";
      return string.Format(format, PercentEncode(_auth.ConsumerSecret), PercentEncode(_auth.AccessTokenSecret));
    }

    public IDictionary<string, string> GetOrderedSigningParameters()
    {
      return GetOAuthParameters().Union(_requestParameters).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    /// <summary>
    /// Returns a collection of OAuth parameters.  Optionally supply the signature to provide this in the params.
    /// </summary>
    /// <remarks>As we build up the signature based on these params, don't pass it when build the signature otherwise it will be included twice.</remarks>
    /// <param name="signature"></param>
    /// <returns></returns>
    public IDictionary<string, string> GetOAuthParameters(string signature = null)
    {
      IDictionary<string, string> parameters = new Dictionary<string, string>();

      parameters.Add("oauth_consumer_key", _auth.ConsumerKey);
      parameters.Add("oauth_nonce", _nonce);

      if (!string.IsNullOrEmpty(signature))
      {
        parameters.Add("oauth_signature", signature);
      }

      parameters.Add("oauth_signature_method", _signing.Method);
      parameters.Add("oauth_timestamp", _timestamp);
      parameters.Add("oauth_token", _auth.AccessToken);
      parameters.Add("oauth_version", _oAuthVersion);

      return parameters;
    }

    public static void SetAuthorisationHeader(IAuthCredentials auth, Uri uri, HttpMethod method, NameValueCollection responseHeaders, IDictionary<string, string> data = null)
    {
      responseHeaders = responseHeaders ?? new NameValueCollection(0);
      new OAuth(auth, method, uri.ToString(), data).SetAuthorisationHeader(responseHeaders);
    }

    private static string NewNonce()
    {
      return Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
    }

    private static string NewTimestamp()
    {
      return DateTimeExtensions.SecondsSinceEpoch().ToString();
    }

    private string PercentEncode(string stringToEscape)
    {
      return Uri.EscapeDataString(stringToEscape);
    }

    private readonly IOAuthSigning _signing;

    private readonly IAuthCredentials _auth;

    private readonly HttpMethod _method;

    private readonly string _baseUri;

    private readonly IDictionary<string, string> _requestParameters;

    private const string _oAuthVersion = "1.0";

    private readonly string _nonce;

    private readonly string _timestamp;
  }
}