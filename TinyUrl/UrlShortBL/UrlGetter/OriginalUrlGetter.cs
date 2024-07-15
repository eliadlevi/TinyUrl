using TinyUrl.Cache;
using TinyUrl.DB;
using TinyUrl.Exceptions;
using TinyUrl.Models;

namespace TinyUrl.UrlShortBL.UrlGetter
{
    public class OriginalUrlGetter : IOriginalUrlGetter
    {
        private readonly IUrlDbService _urlService;
        private readonly ICache<string, Url> _cache;
        private readonly ILogger _logger;
        public OriginalUrlGetter(ICache<string, Url> cache,
            IUrlDbService urlService,
            ILogger logger)
        {
            _cache = cache;
            _urlService = urlService;
            _logger = logger;
        }

        // Getting the value of the object with the shortUrl key first from cache
        // If the object is not in the cache
        // Getting the object form the db and return 
        public async Task<Url> GetOriginalUrl(string shortUrl)
        {
            _logger.LogInformation("log");
            var shortUrlClean = shortUrl.Split('/')[^1];

            var cacheResult = _cache.Get(shortUrlClean);
            if (cacheResult != null)
            {
                return cacheResult;
            }
            var dbResult = await _urlService.GetUrlByShortUrlAsync(shortUrlClean);
            if (dbResult != null)
            {
                if (!_cache.Add(shortUrlClean, dbResult))
                {
                    _logger.LogWarning($"Failed at removing the shortUrl from the cache {shortUrl}");
                }
                return dbResult;
            }

            throw new UrlNotFoundException("Short Url given does not exist in db");
        }
    }
}
