using System;
using System.Collections.Generic;

namespace BirdFeed.Core
{
  internal interface IHttpClient
  {
    T Get<T>(Uri uri, IDictionary<string, string> data = null);

    T Get<T>(Uri uri, Func<string, T> serializer, IDictionary<string, string> data = null);

    void Post(Uri uri, IDictionary<string, string> data = null);

    T Post<T>(Uri uri, IDictionary<string, string> data = null);

    T Post<T>(Uri uri, Func<string, T> serializer, IDictionary<string, string> data = null);

    void Put(Uri uri, IDictionary<string, string> data = null);

    T Put<T>(Uri uri, IDictionary<string, string> data = null);

    T Put<T>(Uri uri, Func<string, T> serializer, IDictionary<string, string> data = null);

    bool DeCompress { get; set; }

    event HttpClientEventHandler PreResponse;
  }
}