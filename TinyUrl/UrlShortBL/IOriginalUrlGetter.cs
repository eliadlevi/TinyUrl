using TinyUrl.Models;

namespace TinyUrl.UrlShortBL
{
    public interface IOriginalUrlGetter
    {
        Task<Url> GetOriginalUrl(string shortUrl);
    }
}