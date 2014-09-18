using BirdFeed.Core.Attributes;
using System;

namespace BirdFeed.Core.Request.Options
{
  public class UpdateStatusOptions : ApiOptions
  {
    public UpdateStatusOptions(string status)
    {
      if (string.IsNullOrEmpty(status))
        throw new ArgumentNullException("status");

      Status = status;
    }

    [Option("status")]
    public string Status { get; set; }
  }
}