using TinyUrl.DB;
using TinyUrl.Exceptions;
using TinyUrl.Models;
using TinyUrl.UrlShortBL.UrlShortning;

namespace TinyUrl.UrlShortBL.CreateShortUrl
{
    public class ShortUrlCreator : IShortUrlCreator
    {
        private readonly IShortUrl _shortUrl;
        private readonly IUrlDbService _urlService;

        public ShortUrlCreator(IShortUrl shortUrl,
            IUrlDbService urlSerive)
        {
            _shortUrl = shortUrl;
            _urlService = urlSerive;
        }

        public async Task<Url> RunAsync(string originalUrl)
        {
            if (!IsValidUrl(originalUrl))
            {
                throw new NotAValidUrlException("The url is not a valid url");
            }

            var shortUrl = _shortUrl.CreateShortUrl(originalUrl);

            Url url = new()
            {
                OriginalUrl = originalUrl,
                ShortUrl = shortUrl
            };
            return await _urlService.AddUrlIfNotExist(url);
        }

        private static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        }
    }
}
