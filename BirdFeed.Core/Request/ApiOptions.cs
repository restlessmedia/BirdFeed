using BirdFeed.Core.Extensions.Collections;
using System.Collections.Generic;

namespace BirdFeed.Core.Request
{
  public abstract class ApiOptions
  {
    public IDictionary<string, string> Parameters
    {
      get
      {
        return this.PropertiesToDictionary();
      }
    }
  }
}