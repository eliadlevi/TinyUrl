using TinyUrl.Models;

namespace TinyUrl.DB
{
    public interface IUrlDbService
    {
        Task<Url?> GetUrlByShortUrlAsync(string shortUrl);

        Task<Url> AddUrlIfNotExist(Url url);
    }
}