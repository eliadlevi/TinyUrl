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

        private bool IsValidUrl(string url)
        {
            Uri uriResult;

            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        }
    }
}
