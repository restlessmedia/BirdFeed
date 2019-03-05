using BirdFeed.Core.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BirdFeed.Core
{
  public interface ITwitterHttpClient
  {
    TResult Get<TResult>(string uri, IApiOptions options);

    TResult Get<TResult>(string uri, IApiOptions options, Func<string, TResult> serializer);

    TResult Post<TResult>(string uri, IApiOptions options);

    TResult Post<TResult>(string uri, IApiOptions options, Func<string, TResult> serializer);

    IEnumerable<TResult> Query<TResult>(string uri, IApiOptions options, HttpMethod method);

    IEnumerable<TResult> Query<TResult>(string uri, IApiOptions options, HttpMethod method, Func<string, IEnumerable<TResult>> serializer);
  }
}