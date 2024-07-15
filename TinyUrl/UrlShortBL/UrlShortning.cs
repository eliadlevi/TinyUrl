using TinyUrl.DB;
using TinyUrl.Exceptions;
using TinyUrl.Models;

namespace TinyUrl.UrlShortBL
{
    // Using a checksum to Shortining the url
    public class UrlShortning : IUrlShortning
    {
        private readonly IShortUrl _shortUrl;
        private readonly IUrlDbService _urlService;

        public UrlShortning(IShortUrl shortUrl,
            IUrlDbService urlSerive)
        {
            _shortUrl = shortUrl;
            _urlService = urlSerive;
        }

        public async Task<Url> RunAsync(string originalUrl)
        {
            if (!isValidUrl(originalUrl))
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

        private bool isValidUrl(string url)
        {
            Uri uriResult;

            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        }
    }
}
