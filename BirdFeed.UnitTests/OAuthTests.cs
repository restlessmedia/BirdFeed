using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdFeed.Core;
using FakeItEasy;
using System.Net.Http;
using System.Collections.Generic;
using Should;
using System.Collections.Specialized;

namespace BirdFeed.UnitTests
{
  [TestClass]
  public class OAuthTests
  {
    public OAuthTests()
    {
      _auth = A.Fake<IAuthCredentials>();
      _baseUri = "https://api.twitter.com/1/statuses/update.json";
      _oAuth = new OAuth(_auth, HttpMethod.Post, _baseUri, _requestParameters, _nonce, _timestamp);

      A.CallTo(() => _auth.ConsumerKey).Returns(_consumerKey);
      A.CallTo(() => _auth.ConsumerSecret).Returns(_consumerSecret);
      A.CallTo(() => _auth.AccessToken).Returns(_accessToken);
      A.CallTo(() => _auth.AccessTokenSecret).Returns(_accessTokenSecret);
    }

    [TestMethod]
    public void GetOAuthParameters_returns_auth_parameters_without_signature()
    {
      IDictionary<string, string> authParameters = _oAuth.GetOAuthParameters();
      authParameters.Count.ShouldEqual(6);
      authParameters.Keys.Contains("oauth_consumer_key").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_nonce").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_signature_method").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_timestamp").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_token").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_version").ShouldBeTrue();
    }

    [TestMethod]
    public void GetOAuthParameters_returns_auth_parameters_with_signature()
    {
      const string signature = "foobar";
      IDictionary<string, string> authParameters = _oAuth.GetOAuthParameters(signature);
      authParameters.Count.ShouldEqual(7);
      authParameters.Keys.Contains("oauth_consumer_key").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_nonce").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_signature").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_signature_method").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_timestamp").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_token").ShouldBeTrue();
      authParameters.Keys.Contains("oauth_version").ShouldBeTrue();

      authParameters["oauth_signature"].ShouldEqual(signature);
    }

    [TestMethod]
    public void GetOrderedSigningParameters_returns_unioned_parameters()
    {
      _oAuth.GetOrderedSigningParameters().Count.ShouldEqual(8);
      _oAuth.GetOrderedSigningParameters()["include_entities"].ShouldEqual(_requestParameters["include_entities"]);
      _oAuth.GetOrderedSigningParameters()["status"].ShouldEqual(_requestParameters["status"]);
    }

    [TestMethod]
    public void SignatureBaseString_returns_value()
    {
      _oAuth.SignatureBaseString().ShouldEqual("POST&https%3A%2F%2Fapi.twitter.com%2F1%2Fstatuses%2Fupdate.json&include_entities%3Dtrue%26oauth_consumer_key%3Dxvz1evFS4wEEPTGEFPHBog%26oauth_nonce%3DkYjzVBB8Y0ZFabxSWbWovY3uYSQ2pTgmZeNu2VS4cg%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1318622958%26oauth_token%3D370773112-GmHxMAgYyLbNEtIKZeRNFsMKPR9EyMZeS9weJAEb%26oauth_version%3D1.0%26status%3DHello%2520Ladies%2520%252B%2520Gentlemen%252C%2520a%2520signed%2520OAuth%2520request%2521");
    }

    [TestMethod]
    public void SigningKey_returns_value()
    {
      _oAuth.SigningKey().ShouldEqual("kAcSOqF21Fu85e7zjz7ZN2U4ZRhfV3WpwPAoE3Z7kBw&LswwdoUaIvS8ltyTt5jkRh4J50vUPVVHtR2YPi5kE");
    }

    [TestMethod]
    public void Signature_returns_value()
    {
      _oAuth.Signature().ShouldEqual("tnnArxj06cWHq44gCs1OSKk/jLY=");
    }

    [TestMethod]
    public void AuthHeader_returns_value()
    {
      _oAuth.AuthHeader().ShouldEqual("OAuth oauth_consumer_key=\"xvz1evFS4wEEPTGEFPHBog\", oauth_nonce=\"kYjzVBB8Y0ZFabxSWbWovY3uYSQ2pTgmZeNu2VS4cg\", oauth_signature=\"tnnArxj06cWHq44gCs1OSKk%2FjLY%3D\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"1318622958\", oauth_token=\"370773112-GmHxMAgYyLbNEtIKZeRNFsMKPR9EyMZeS9weJAEb\", oauth_version=\"1.0\"");
    }

    [TestMethod]
    public void SetAuthorisationHeader_sets_header()
    {
      NameValueCollection headers = new NameValueCollection();

      _oAuth.SetAuthorisationHeader(headers);

      headers.AllKeys.ShouldContain("Authorization");
      headers["Authorization"].ShouldEqual(_oAuth.AuthHeader());
    }

    private readonly OAuth _oAuth;

    private readonly IAuthCredentials _auth;

    private readonly IDictionary<string, string> _requestParameters = new Dictionary<string, string>
        {
            { "include_entities", "true"},
            { "status", "Hello Ladies + Gentlemen, a signed OAuth request!"}
        };

    private readonly string _baseUri = "https://api.twitter.com/1/statuses/update.json";

    private const string _consumerKey = "xvz1evFS4wEEPTGEFPHBog";

    private const string _consumerSecret = "kAcSOqF21Fu85e7zjz7ZN2U4ZRhfV3WpwPAoE3Z7kBw";

    private const string _accessToken = "370773112-GmHxMAgYyLbNEtIKZeRNFsMKPR9EyMZeS9weJAEb";

    private const string _accessTokenSecret = "LswwdoUaIvS8ltyTt5jkRh4J50vUPVVHtR2YPi5kE";

    private const string _nonce = "kYjzVBB8Y0ZFabxSWbWovY3uYSQ2pTgmZeNu2VS4cg";

    private const string _timestamp = "1318622958";
  }
}