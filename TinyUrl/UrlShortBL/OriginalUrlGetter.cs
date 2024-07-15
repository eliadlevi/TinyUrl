using TinyUrl.DB;
using TinyUrl.Models;

namespace TinyUrl.UrlShortBL
{
    public class OriginalUrlGetter : IOriginalUrlGetter
    {
        private readonly IUrlDbService _urlService;

        public OriginalUrlGetter(IUrlDbService urlService)
        {
            _urlService = urlService;
        }

        public async Task<Url> GetOriginalUrl(string shortUrl)
        {
            var shortUrlClean = shortUrl.Split('/')[^1];
            var s = await _urlService.GetUrlByShortUrlAsync(shortUrlClean);
            return s;
        }
    }
}
