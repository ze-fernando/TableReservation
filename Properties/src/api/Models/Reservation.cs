namespace src.api.Models
{
    class Reservation
    {
        public required int NumberPeople { get; set; }
        public required DateTime Date { get; set; }
        public required string ReservationName { get; set; }
        public required User ReservationUser { get; set; }
    }
}
