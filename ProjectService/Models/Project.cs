using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjectService.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("charts")]
        public List<Chart> Charts { get; set; }
    }
}
