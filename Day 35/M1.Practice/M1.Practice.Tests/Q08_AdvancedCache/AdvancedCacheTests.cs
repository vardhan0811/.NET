using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q08_AdvancedCache;

namespace M1.Practice.Tests.Q08_AdvancedCache
{
    [TestClass]
    public class AdvancedCacheTests
    {
        [TestMethod]
        public void Get_ShouldReturnValue()
        {
            var cache =
                new AdvancedCache<string, int>(3);

            cache.Set("A", 10, 10);

            var val = cache.Get("A");

            Assert.AreEqual(10, val);
        }

        [TestMethod]
        public void ExpiredItem_ShouldReturnDefault()
        {
            var cache =
                new AdvancedCache<string, int>(3);

            cache.Set("A", 10, 1);

            Thread.Sleep(1500);

            var val = cache.Get("A");

            Assert.AreEqual(0, val);
        }

        [TestMethod]
        public void LruEviction_ShouldRemoveOldest()
        {
            var cache =
                new AdvancedCache<string, int>(2);

            cache.Set("A", 1, 10);
            cache.Set("B", 2, 10);

            // Access A (now B is LRU)
            cache.Get("A");

            cache.Set("C", 3, 10);

            Assert.AreEqual(0, cache.Get("B"));
            Assert.AreEqual(1, cache.Get("A"));
            Assert.AreEqual(3, cache.Get("C"));
        }
    }
}
