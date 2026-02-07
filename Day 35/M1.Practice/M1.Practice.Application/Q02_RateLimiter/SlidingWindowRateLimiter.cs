using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace M1.Practice.Application.Q02_RateLimiter
{
    public class SlidingWindowRateLimiter
    {
        private readonly int _maxRequests = 5;
        private readonly TimeSpan _window =
            TimeSpan.FromSeconds(10);

        private readonly ConcurrentDictionary<
            string,
            Queue<DateTime>> _requests =
            new();

        private readonly object _lock = new object();

        public bool AllowRequest(
            string clientId,
            DateTime now)
        {
            var queue = _requests.GetOrAdd(
                clientId,
                _ => new Queue<DateTime>());

            lock (_lock)
            {
                // Remove expired
                while (queue.Count > 0 &&
                       now - queue.Peek() > _window)
                {
                    queue.Dequeue();
                }

                if (queue.Count >= _maxRequests)
                    return false;

                queue.Enqueue(now);
                return true;
            }
        }
    }
}
