using BirdFeed.Core.Exceptions;
using BirdFeed.Core.Extensions.Collections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BirdFeed.Core
{
  public class HttpClient : IHttpClient
  {
    public HttpClient(bool deCompress = true)
    {
      DeCompress = deCompress;
    }

    public T Get<T>(Uri uri, IDictionary<string, string> data = null)
    {
      if (uri == null)
        throw new ArgumentNullException("uri");

      using (Client client = CreateClient(uri, HttpMethod.Get, data))
      {
        client.QueryString.Add(data.ToNameValueCollection());

        string result;

        try
        {
          result = client.DownloadString(uri);
        }
        catch (Exception e)
        {
          throw new TwitterHttpException(ExceptionCode.BadGetRequest, e);
        }

        return JsonConvert.DeserializeObject<T>(result);
      }
    }

    public void Post(Uri uri, IDictionary<string, string> data = null)
    {
      Upload(uri, HttpMethod.Post, data);
    }

    public T Post<T>(Uri uri, IDictionary<string, string> data = null)
    {
      return Upload<T>(uri, HttpMethod.Post, data);
    }

    public void Put(Uri uri, IDictionary<string, string> data = null)
    {
      Upload(uri, HttpMethod.Put, data);
    }

    public T Put<T>(Uri uri, IDictionary<string, string> data = null)
    {
      return Upload<T>(uri, HttpMethod.Put, data);
    }

    public event HttpClientEventHandler OnPreResponse;

    public bool DeCompress { get; set; }

    private Client CreateClient(Uri uri, HttpMethod method, IDictionary<string, string> data = null)
    {
      Client client = new Client();

      if (OnPreResponse != null)
        OnPreResponse(uri, method, client.Headers, data);

      return client;
    }

    private void Upload(Uri uri, HttpMethod method, IDictionary<string, string> data = null)
    {
      if (uri == null)
        throw new ArgumentNullException("uri");

      using (Client client = CreateClient(uri, method, data))
      {
        client.UploadValues(uri, method.ToString(), data.ToNameValueCollection());
      }
    }

    private T Upload<T>(Uri uri, HttpMethod method, IDictionary<string, string> data = null)
    {
      if (uri == null)
        throw new ArgumentNullException("uri");

      using (Client client = CreateClient(uri, method, data))
      {
        byte[] response;

        try
        {
          response = client.UploadValues(uri, method.ToString(), data.ToNameValueCollection());
        }
        catch (Exception e)
        {
          throw new TwitterHttpException(ExceptionCode.BadUploadRequest, e);
        }

        string json = Encoding.UTF8.GetString(response);
        return JsonConvert.DeserializeObject<T>(json);
      }
    }

    private class Client : WebClient
    {
      public Client(bool deCompress = true)
      {
        _deCompress = deCompress;
      }

      protected override WebRequest GetWebRequest(Uri address)
      {
        WebRequest request = base.GetWebRequest(address);

        if (!_deCompress)
          return request;

        ((HttpWebRequest)request).AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

        return request;
      }

      private readonly bool _deCompress;
    }
  }
}