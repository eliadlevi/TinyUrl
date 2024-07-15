namespace TinyUrl.UrlShortBL.UrlShortning
{
    public interface IShortUrl
    {
        string CreateShortUrl(string originalUrl);
    }
}