using TinyUrl.Models;

namespace TinyUrl.UrlShortBL.UrlGetter
{
    public interface IOriginalUrlGetter
    {
        Task<Url> GetOriginalUrl(string shortUrl);
    }
}