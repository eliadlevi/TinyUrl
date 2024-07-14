using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TinyUrl.Models
{
    public class Url
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string OriginalUrl { get; set; } = null!;
        public string ShortUrl { get; set; } = null!;

    }
}