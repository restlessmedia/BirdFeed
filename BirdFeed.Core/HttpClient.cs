using BirdFeed.Core.Extensions.Collections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BirdFeed.Core
{
  internal class HttpClient : IHttpClient
  {
    public HttpClient(bool deCompress = true)
    {
      DeCompress = deCompress;
    }

    public T Get<T>(Uri uri, IDictionary<string, string> data = null)
    {
      return Get<T>(uri, json => DefaultSerializer<T>(json), data);
    }

    public T Get<T>(Uri uri, Func<string, T> serializer, IDictionary<string, string> data = null)
    {
      if (uri == null)
      {
        throw new ArgumentNullException("uri");
      }

      using (Client client = CreateClient(uri, HttpMethod.Get, data))
      {
        client.QueryString.Add(data.ToNameValueCollection());

        string result = client.DownloadString(uri);

        return serializer(result);
      }
    }

    public void Post(Uri uri, IDictionary<string, string> data = null)
    {
      Upload(uri, HttpMethod.Post, data);
    }

    public T Post<T>(Uri uri, IDictionary<string, string> data = null)
    {
      return Post<T>(uri, json => DefaultSerializer<T>(json), data);
    }

    public T Post<T>(Uri uri, Func<string, T> serializer, IDictionary<string, string> data = null)
    {
      return Upload<T>(uri, HttpMethod.Post, serializer, data);
    }

    public void Put(Uri uri, IDictionary<string, string> data = null)
    {
      Upload(uri, HttpMethod.Put, data);
    }

    public T Put<T>(Uri uri, IDictionary<string, string> data = null)
    {
      return Put<T>(uri, json => DefaultSerializer<T>(json), data);
    }

    public T Put<T>(Uri uri, Func<string, T> serializer, IDictionary<string, string> data = null)
    {
      return Upload<T>(uri, HttpMethod.Put, serializer, data);
    }

    public event HttpClientEventHandler PreResponse;

    public bool DeCompress { get; set; }

    private Client CreateClient(Uri uri, HttpMethod method, IDictionary<string, string> data = null)
    {
      Client client = new Client();

      if (PreResponse != null)
      {
        PreResponse(uri, method, client.Headers, data);
      }

      return client;
    }

    private void Upload(Uri uri, HttpMethod method, IDictionary<string, string> data = null)
    {
      if (uri == null)
      {
        throw new ArgumentNullException("uri");
      }

      using (Client client = CreateClient(uri, method, data))
      {
        client.UploadValues(uri, method.ToString(), data.ToNameValueCollection());
      }
    }

    private T Upload<T>(Uri uri, HttpMethod method, Func<string, T> serializer, IDictionary<string, string> data = null)
    {
      if (uri == null)
      {
        throw new ArgumentNullException("uri");
      }

      using (Client client = CreateClient(uri, method, data))
      {
        byte[] response = client.UploadValues(uri, method.ToString(), data.ToNameValueCollection());

        return serializer(Encoding.UTF8.GetString(response));
      }
    }

    private T DefaultSerializer<T>(string json)
    {
      return JsonConvert.DeserializeObject<T>(json);
    }

    private class Client : WebClient
    {
      public Client(bool deCompress = true)
      {
        _deCompress = deCompress;
      }

      protected override WebRequest GetWebRequest(Uri address)
      {
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);

        request.ProtocolVersion = HttpVersion.Version10;

        if (!_deCompress)
        {
          return request;
        }

        request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

        return request;
      }

      private readonly bool _deCompress;
    }
  }
}
