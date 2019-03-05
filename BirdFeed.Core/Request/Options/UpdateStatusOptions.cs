using System;
using System.Collections.Generic;

namespace BirdFeed.Core.Request.Options
{
  public class UpdateStatusOptions : IApiOptions
  {
    public UpdateStatusOptions(string status)
    {
      if (string.IsNullOrEmpty(status))
      {
        throw new ArgumentNullException("status");
      }

      Status = status;
    }

    public string Status { get; private set; }

    public IDictionary<string, string> Parameters
    {
      get
      {
        return new Dictionary<string, string>
        {
            {"status",Status}
        };
      }
    }
  }
}