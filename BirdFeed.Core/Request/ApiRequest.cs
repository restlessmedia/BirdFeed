using BirdFeed.Core.Request.Options;
using System;
using System.Net.Http;

namespace BirdFeed.Core.Request
{
  public class ApiRequest<TOptions>
    where TOptions : ApiOptions
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