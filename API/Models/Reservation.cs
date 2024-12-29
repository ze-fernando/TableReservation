using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace API.Models;


public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public required int NumberPeople { get; set; }
    public required DateTime Date { get; set; }
    public required string ReservationName { get; set; }
    public required string EmailOfClient { get; set; }

    public override string ToString() =>
        $@"
        <div style='border: 1px solid #ddd; padding: 10px; margin: 10px;'>
            <p><strong>Data da reserva</strong> {Date:dd/MM/yyyy}</p>
            <p><strong>Mesa para</strong> {NumberPeople} <strong> pessoas</strong></p>
            <p><strong>Reserva no nome de</strong> {ReservationName}</p>
        </div>";
}
