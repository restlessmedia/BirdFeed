using BirdFeed.Core.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BirdFeed.Core
{
  internal class TwitterHttpClient : ITwitterHttpClient
  {
    public TwitterHttpClient(IHttpClient httpClient, IAuthCredentials auth)
    {
      _httpClient = httpClient ?? throw new ArgumentNullException("httpClient");
      _auth = auth ?? throw new ArgumentNullException("auth");
      httpClient.PreResponse += (uri, method, responseHeaders, data) => OAuth.SetAuthorisationHeader(_auth, uri, method, responseHeaders, data);
    }

    public TResult Get<TResult>(string uri, IApiOptions options)
    {
      return _httpClient.Get<TResult>(ParseAssertUri(uri), options.Parameters);
    }

    public TResult Get<TResult>(string uri, IApiOptions options, Func<string, TResult> serializer)
    {
      return _httpClient.Get<TResult>(ParseAssertUri(uri), serializer, options.Parameters);
    }

    public TResult Post<TResult>(string uri, IApiOptions options)
    {
      return _httpClient.Post<TResult>(ParseAssertUri(uri), options.Parameters);
    }

    public TResult Post<TResult>(string uri, IApiOptions options, Func<string, TResult> serializer)
    {
      return _httpClient.Post<TResult>(ParseAssertUri(uri), serializer, options.Parameters);
    }

    public IEnumerable<TResult> Query<TResult>(string uri, IApiOptions options, HttpMethod method)
    {
      if (method == HttpMethod.Post)
      {
        return Post<IEnumerable<TResult>>(uri, options);
      }

      return Get<IEnumerable<TResult>>(uri, options);
    }

    public IEnumerable<TResult> Query<TResult>(string uri, IApiOptions options, HttpMethod method, Func<string, IEnumerable<TResult>> serializer)
    {
      if (method == HttpMethod.Post)
      {
        return Post<IEnumerable<TResult>>(uri, options, serializer);
      }

      return Get<IEnumerable<TResult>>(uri, options, serializer);
    }

    private Uri ParseAssertUri(string uri)
    {
      Uri parsedUri;

      if (string.IsNullOrEmpty(uri) || !Uri.TryCreate(uri, UriKind.Absolute, out parsedUri))
      {
        throw new InvalidCastException("Invalid uri");
      }

      return parsedUri;
    }

    private readonly IAuthCredentials _auth;

    private readonly IHttpClient _httpClient;
  }
}