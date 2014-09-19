using BirdFeed.Core.Request.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace BirdFeed.UnitTests.Request.Options
{
    [TestClass]
    public class UpdateStatusOptionsTests
    {
        [TestMethod]
        public void parameters_has_count()
        {
            const string status = "the quick brown fox jumped.";

            new UpdateStatusOptions(status).Parameters["status"].ShouldEqual(status);
        }
    }
}