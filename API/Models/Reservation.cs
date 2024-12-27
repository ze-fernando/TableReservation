using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Models
{
    class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required int NumberPeople { get; set; }
        public required DateTime Date { get; set; }
        public required string ReservationName { get; set; }
        public required string EmailOfClient { get; set; }
        public required bool IsConfirmed { get; set; }
    }
}
