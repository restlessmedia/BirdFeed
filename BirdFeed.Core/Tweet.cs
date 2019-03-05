using BirdFeed.Core.Extensions;
using System;

namespace BirdFeed.Core
{
  public class Tweet
  {
    public string Text { get; set; }

    public DateTime Created { get; set; }

    public string Html
    {
      get
      {
        return string.IsNullOrEmpty(Text) ? Text : Text.FormatUrls();
      }
    }
  }
}