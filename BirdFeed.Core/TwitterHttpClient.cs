using System;
using System.Collections.Generic;
using System.Net.Http;
using BirdFeed.Core.Request;
using BirdFeed.Core.Request.Options;

namespace BirdFeed.Core
{
  public class TwitterHttpClient : ITwitterHttpClient
  {
    public TwitterHttpClient(IHttpClient httpClient, IAuthCredentials auth)
    {
      if (httpClient == null)
        throw new ArgumentNullException("httpClient");

      if (auth == null)
        throw new ArgumentNullException("auth");

      _httpClient = httpClient;
      _auth = auth;
      httpClient.OnPreResponse += (uri, method, responseHeaders, data) => OAuth.SetAuthorisationHeader(_auth, uri, method, responseHeaders, data);
    }

    public TResult Get<TOptions, TResult>(string uri, TOptions options)
      where TOptions : ApiOptions
    {
      Uri parsedUri;

      if (!Uri.TryCreate(uri, UriKind.Absolute, out parsedUri))
        throw new InvalidCastException("Unable to cast uri");

      return Get<TOptions, TResult>(parsedUri, options);
    }

    public TResult Get<TOptions, TResult>(Uri uri, TOptions options)
      where TOptions : ApiOptions
    {
      return _httpClient.Get<TResult>(uri, options.Parameters);
    }

    public TResult Post<TOptions, TResult>(string uri, TOptions options)
      where TOptions : ApiOptions
    {
      Uri parsedUri;

      if (!Uri.TryCreate(uri, UriKind.Absolute, out parsedUri))
        throw new InvalidCastException("Unable to cast uri");

      return Post<TOptions, TResult>(parsedUri, options);
    }

    public TResult Post<TOptions, TResult>(Uri uri, TOptions options)
      where TOptions : ApiOptions
    {
      return _httpClient.Post<TResult>(uri, options.Parameters);
    }

    public IEnumerable<TResult> Query<TOptions, TResult>(string uri, TOptions options, HttpMethod method)
      where TOptions : ApiOptions
    {
      if (method == HttpMethod.Post)
        return Post<TOptions, IEnumerable<TResult>>(uri, options);

      return Get<TOptions, IEnumerable<TResult>>(uri, options);
    }

    public IEnumerable<TResult> Query<TOptions, TResult>(Uri uri, TOptions options, HttpMethod method)
      where TOptions : ApiOptions
    {
      if (method == HttpMethod.Post)
        return Post<TOptions, IEnumerable<TResult>>(uri, options);

      return Get<TOptions, IEnumerable<TResult>>(uri, options);
    }

    private readonly IAuthCredentials _auth;

    private readonly IHttpClient _httpClient;
  }
}