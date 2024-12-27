namespace Dtos
{
    public record ReservationDto(
            int NumberOfPeople,
            string ReservationName,
            string Email,
            DateTime Date
            );
}
