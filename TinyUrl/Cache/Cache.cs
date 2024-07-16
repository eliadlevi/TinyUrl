using System.Collections.Concurrent;
using TinyUrl.Logger;

namespace TinyUrl.Cache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        // Manage the size of the of the cache by amount of items
        private readonly int _maxSize;

        // To manage the object to remove by useablity
        private readonly ConcurrentQueue<TKey> _queue = new();

        // Store the object of the cache
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary = new();
        private readonly ILog _log;

        // The size of the cache is dependent on the client and can change according to the mechine capabilities and the service Needs.
        // The object removal Least used object removed first.
        public Cache(int maxSize, ILog logger)
        {
            _maxSize = maxSize;
            _log = logger;
        }

        public TValue? Get(TKey key)
        {
            if (key == null) return default;

            if (_dictionary.TryGetValue(key, out var node))
            {
                if (_queue.TryDequeue(out var queueObject))
                {
                    _queue.Enqueue(queueObject);

                    return node;
                }

                return default;
            }
            return default;
        }

        public bool Add(TKey key, TValue value)
        {
            if (key == null || value == null) return false;
            if (_dictionary.Count >= _maxSize)
            {
                if (!_queue.TryDequeue(out var queueKey))
                {
                    _log.LogWarning($"Couldn't remove key: {key} from the cache");
                    return false;
                }
                if (!_dictionary.Remove(queueKey, out var removeDicResult))
                {
                    _log.LogWarning($"Couldn't remove key: {key} from the dictionary");

                    return false;
                }
            }

            _dictionary.AddOrUpdate(key, (key) =>
            {
                _queue.Enqueue(key);
                return value;
            }, (key, value) => value);
            return true;
        }
    }
}
