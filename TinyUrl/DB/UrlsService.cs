using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TinyUrl.Models;

namespace TinyUrl.DB
{
    public class UrlsService
    {
        private readonly IMongoCollection<Url> _urlShortsCollection;

        public UrlsService(
            IOptions<UrlShortsDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _urlShortsCollection = mongoDatabase.GetCollection<Url>(
                bookStoreDatabaseSettings.Value.UrlShortsCollectionName);
        }

        public async Task<Url?> GetUrlShortByOriginalUrlAsync(string originalUrl)
        {
            return await _urlShortsCollection.Find(x => x.OriginalUrl == originalUrl).FirstOrDefaultAsync();
        }

        public async Task<Url?> GetUrlShortByShortUrlAsync(string shortUrl)
        {
            return await _urlShortsCollection.Find(x => x.ShortUrl == shortUrl).FirstOrDefaultAsync();
        }

    }
}
