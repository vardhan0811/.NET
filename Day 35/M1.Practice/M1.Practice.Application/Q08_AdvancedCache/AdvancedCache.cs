using System;
using System.Collections.Generic;

namespace M1.Practice.Application.Q08_AdvancedCache
{
    public class AdvancedCache<TKey, TValue>
    {
        private class CacheItem
        {
            public TKey Key;
            public TValue Value;
            public DateTime Expiry;
        }

        private readonly int _capacity;

        private readonly Dictionary<TKey, LinkedListNode<CacheItem>>
            _map = new();

        private readonly LinkedList<CacheItem>
            _lruList = new();

        private readonly object _lock = new();

        // -----------------------------------

        public AdvancedCache(int capacity)
        {
            _capacity = capacity;
        }

        // -----------------------------------

        public void Set(
            TKey key,
            TValue value,
            int ttlSeconds)
        {
            lock (_lock)
            {
                if (_map.ContainsKey(key))
                {
                    Remove(key);
                }

                if (_map.Count >= _capacity)
                {
                    RemoveLeastUsed();
                }

                var item = new CacheItem
                {
                    Key = key,
                    Value = value,
                    Expiry =
                        DateTime.UtcNow.AddSeconds(ttlSeconds)
                };

                var node =
                    new LinkedListNode<CacheItem>(item);

                _lruList.AddFirst(node);
                _map[key] = node;
            }
        }

        // -----------------------------------

        public TValue Get(TKey key)
        {
            lock (_lock)
            {
                if (!_map.TryGetValue(key, out var node))
                    return default;

                if (node.Value.Expiry < DateTime.UtcNow)
                {
                    Remove(key);
                    return default;
                }

                // Move to front (recently used)
                _lruList.Remove(node);
                _lruList.AddFirst(node);

                return node.Value.Value;
            }
        }

        // -----------------------------------

        private void RemoveLeastUsed()
        {
            var last = _lruList.Last;

            if (last != null)
            {
                _map.Remove(last.Value.Key);
                _lruList.RemoveLast();
            }
        }

        private void Remove(TKey key)
        {
            if (_map.TryGetValue(key, out var node))
            {
                _lruList.Remove(node);
                _map.Remove(key);
            }
        }
    }
}
