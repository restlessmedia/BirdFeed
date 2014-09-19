using BirdFeed.Core.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BirdFeed.Core
{
  public interface ITwitterHttpClient
  {
    TResult Get<TOptions, TResult>(string uri, TOptions options)
      where TOptions : IApiOptions;

    TResult Get<TOptions, TResult>(Uri uri, TOptions options)
      where TOptions : IApiOptions;

    TResult Post<TOptions, TResult>(string uri, TOptions options)
      where TOptions : IApiOptions;

    TResult Post<TOptions, TResult>(Uri uri, TOptions options)
      where TOptions : IApiOptions;

    IEnumerable<TResult> Query<TOptions, TResult>(string uri, TOptions options, HttpMethod method)
      where TOptions : IApiOptions;

    IEnumerable<TResult> Query<TOptions, TResult>(Uri uri, TOptions options, HttpMethod method)
      where TOptions : IApiOptions;
  }
}