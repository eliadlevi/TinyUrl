using TinyUrl.Models;

namespace TinyUrl.UrlShortBL.CreateShortUrl
{
    public interface IShortUrlCreator
    {
        Task<Url> RunAsync(string originalUrl);
    }
}