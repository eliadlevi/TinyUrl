using TinyUrl.Models;

namespace TinyUrl.UrlShortBL
{
    public interface IUrlShortning
    {
        Task<Url> RunAsync(string originalUrl);
    }
}