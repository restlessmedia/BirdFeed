using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;

namespace BirdFeed.Core
{
  public delegate void HttpClientEventHandler(Uri uri, HttpMethod method, NameValueCollection responseHeaders, IDictionary<string, string> data = null);
}