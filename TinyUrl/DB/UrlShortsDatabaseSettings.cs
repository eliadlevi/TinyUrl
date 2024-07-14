namespace TinyUrl.DB
{
    public class UrlShortsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UrlShortsCollectionName { get; set; } = null!;
    }
}
