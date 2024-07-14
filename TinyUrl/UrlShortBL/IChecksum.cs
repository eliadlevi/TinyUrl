namespace TinyUrl.UrlShortBL
{
    public interface IChecksum
    {
        string Run(string value);
    }
}