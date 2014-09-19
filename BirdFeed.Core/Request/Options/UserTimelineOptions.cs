using BirdFeed.Core.Extensions;
using System;
using System.Collections.Generic;

namespace BirdFeed.Core.Request.Options
{
    public class UserTimelineOptions : IApiOptions
    {
        public UserTimelineOptions(int userId, int count)
        {
            if (count == 0)
                throw new ArgumentException("count", string.Format("Invalid value {0} for count", count));

            UserId = userId;
            Count = count;
        }

        public UserTimelineOptions(string username, int count)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            if (count == 0)
                throw new ArgumentException("count", string.Format("Invalid value {0} for count", count));

            Username = username;
            Count = count;
        }

        public UserTimelineOptions(int userId)
            : this(userId, 10) { }

        public UserTimelineOptions(string username)
            : this(username, 10) { }

        public int? UserId { get; private set; }

        public string Username { get; private set; }

        public int Count { get; private set; }

        public bool TrimUser { get; set; }

        public bool ExcludeReplies { get; set; }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {UserId.HasValue ? "user_id" : "screen_name" , UserId.HasValue ? UserId.ToString() : Username},
                    {"count", Count.ToString()},
                    {"trim_user", TrimUser.ToApiString()},
                    {"exclude_replies", ExcludeReplies.ToApiString()}
                };
            }
        }
    }
}