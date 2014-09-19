﻿using System;
using System.Net.Http;

namespace BirdFeed.Core.Request
{
  internal sealed class ApiRequest<TOptions>
    where TOptions : IApiOptions
  {
    public ApiRequest(Uri uri, HttpMethod method, TOptions options)
    {
      if (uri == null)
        throw new ArgumentNullException("uri");

      Uri = uri;
      Method = method;
      Options = options;
    }

    public Uri Uri { get; private set; }

    public HttpMethod Method { get; private set; }

    public TOptions Options { get; private set; }
  }
}