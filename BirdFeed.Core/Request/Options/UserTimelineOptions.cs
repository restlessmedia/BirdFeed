using BirdFeed.Core.Attributes;
using System;
using System.ComponentModel;

namespace BirdFeed.Core.Request.Options
{
  public class UserTimelineOptions : ApiOptions
  {
    public UserTimelineOptions(int userId)
    {
      UserId = userId;
    }

    public UserTimelineOptions(string username)
    {
      if (string.IsNullOrEmpty(username))
        throw new ArgumentNullException("name");

      Username = username;
    }

    public UserTimelineOptions(int userId, int count)
      : this(userId)
    {
      Count = count;
    }

    public UserTimelineOptions(string username, int count)
      : this(username)
    {
      Count = count;
    }

    [Option("user_id")]
    public int? UserId { get; set; }

    [Option("screen_name")]
    public string Username { get; set; }

    [Option("count", "10")]
    public int Count { get; set; }

    [Option("trim_user", "1")]
    public bool TrimUser { get; set; }

    [Option("exclude_replies", "1")]
    public bool ExcludeReplies { get; set; }
  }
}