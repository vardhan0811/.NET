using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q02_RateLimiter;

namespace M1.Practice.Tests.Q02_RateLimiter
{
    [TestClass]
    public class RateLimiterTests
    {
        [TestMethod]
        public void AllowOnlyFiveRequests_InWindow()
        {
            var limiter =
                new SlidingWindowRateLimiter();

            var client = "C1";
            var now = DateTime.UtcNow;

            // First 5 allowed
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(
                    limiter.AllowRequest(client, now));
            }

            // 6th denied
            Assert.IsFalse(
                limiter.AllowRequest(client, now));
        }

        [TestMethod]
        public void AllowAfterWindowExpires()
        {
            var limiter =
                new SlidingWindowRateLimiter();

            var client = "C1";
            var now = DateTime.UtcNow;

            for (int i = 0; i < 5; i++)
            {
                limiter.AllowRequest(client, now);
            }

            // After 11 seconds
            var later =
                now.AddSeconds(11);

            Assert.IsTrue(
                limiter.AllowRequest(client, later));
        }
    }
}
