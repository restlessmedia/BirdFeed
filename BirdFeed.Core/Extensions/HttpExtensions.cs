using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace BirdFeed.Core.Extensions
{
  public static class HttpExtensions
  {
    public static IDictionary<string, string> QueryToDictionary(this HttpRequestMessage request)
    {
      return request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
    }
  }
}