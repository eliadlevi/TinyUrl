using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TinyUrl.Exceptions;
using TinyUrl.Models;

namespace TinyUrl.DB
{
    public class UrlsMongoService : IUrlDbService
    {
        private readonly IMongoCollection<Url> _urlShortsCollection;

        public UrlsMongoService(
            IOptions<UrlShortsDatabaseSettings> bookStoreDatabaseSettings)
        {
            try
            {
                var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    bookStoreDatabaseSettings.Value.DatabaseName);

                _urlShortsCollection = mongoDatabase.GetCollection<Url>(
                    bookStoreDatabaseSettings.Value.UrlShortsCollectionName);
            }
            catch (Exception ex)
            {
                throw new DBConnectionException("DB connection failed, please validate your cradentials." + ex.Message);
            }

        }

        public async Task<Url?> GetUrlShortByOriginalUrlAsync(string originalUrl)
        {
            return await _urlShortsCollection.Find(x => x.OriginalUrl == originalUrl).FirstOrDefaultAsync();
        }

        public async Task<Url?> GetUrlShortByShortUrlAsync(string shortUrl)
        {
            return await _urlShortsCollection.Find(x => x.ShortUrl == shortUrl).FirstOrDefaultAsync();
        }

        public async Task<Url> AddUrlIfNotExist(Url url)
        {
            var getUrl = await GetUrlShortByShortUrlAsync(url.ShortUrl);
            if (getUrl == null)
            {
                await _urlShortsCollection.InsertOneAsync(url);
                return url;
            }
            return getUrl;
        }

    }
}
