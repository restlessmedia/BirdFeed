using BirdFeed.Core.Request.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;

namespace BirdFeed.UnitTests.Request.Options
{
    [TestClass]
    public class UserTimelineOptionsTests
    {
        [TestMethod]
        public void parameters_has_userId()
        {
            const int userId = 999;
            UserTimelineOptions options = new UserTimelineOptions(userId);

            options.Parameters.ContainsKey("user_id").ShouldBeTrue();
            options.Parameters.ContainsKey("screen_name").ShouldBeFalse();
            options.Parameters["user_id"].ShouldEqual(userId.ToString());
        }

        [TestMethod]
        public void parameters_has_username()
        {
            string username = Guid.NewGuid().ToString();
            UserTimelineOptions options = new UserTimelineOptions(username);

            options.Parameters.ContainsKey("user_id").ShouldBeFalse();
            options.Parameters.ContainsKey("screen_name").ShouldBeTrue();
            options.Parameters["screen_name"].ShouldEqual(username);
        }

        [TestMethod]
        public void parameters_has_count()
        {
            const int count = 99;
            UserTimelineOptions options;

            options = new UserTimelineOptions("username", count);
            options.Parameters.ContainsKey("count").ShouldBeTrue();
            options.Parameters["count"].ShouldEqual(count.ToString());

            options = new UserTimelineOptions(123, count);
            options.Parameters.ContainsKey("count").ShouldBeTrue();
            options.Parameters["count"].ShouldEqual(count.ToString());
        }

        [TestMethod]
        public void parameters_has_trimuser()
        {
            new UserTimelineOptions("username") { TrimUser = true }.Parameters["trim_user"].ShouldEqual("1");
            new UserTimelineOptions("username") { TrimUser = false }.Parameters["trim_user"].ShouldEqual("0");
        }

        [TestMethod]
        public void parameters_has_excludereplies()
        {
            new UserTimelineOptions("username") { ExcludeReplies = true }.Parameters["exclude_replies"].ShouldEqual("1");
            new UserTimelineOptions("username") { ExcludeReplies = false }.Parameters["exclude_replies"].ShouldEqual("0");
        }
    }
}