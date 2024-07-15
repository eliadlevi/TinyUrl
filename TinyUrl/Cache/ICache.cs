namespace TinyUrl.Cache
{
    public interface ICache<TKey, TValue>
    {
        TValue? Get(TKey key);
        bool Add(TKey key, TValue value);
    }
}