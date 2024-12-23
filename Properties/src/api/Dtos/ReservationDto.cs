namespace src.api.Dtos
{
    public record ReservationDto(
            int NumberOfPeople,
            string ReservationName,
            DateTime Date
            );
}
