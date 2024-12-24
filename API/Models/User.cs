using Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Nickname { get; set; }
        public required string Tel { get; set; }
        public required string Password { get; set; }
        public required Roles Role { get; set; }
    }
}


