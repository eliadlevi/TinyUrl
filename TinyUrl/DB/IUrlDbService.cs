using TinyUrl.Models;

namespace TinyUrl.DB
{
    public interface IUrlDbService
    {
        Task<Url?> GetUrlShortByOriginalUrlAsync(string originalUrl);
        Task<Url?> GetUrlShortByShortUrlAsync(string shortUrl);

        Task<Url> AddUrlIfNotExist(Url url);
    }
}