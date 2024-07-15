namespace TinyUrl.UrlShortBL.Checksum
{
    public interface IChecksum
    {
        string Run(string value);
    }
}