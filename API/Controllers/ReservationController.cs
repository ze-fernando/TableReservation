using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Dtos;
using API.Models;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class ReservationController(ReservationService service,
        RedisService cache, EmailService mail) : ControllerBase
{

    private readonly ReservationService _service = service;
    private readonly RedisService _cache = cache;
    private readonly EmailService _mail = mail;

    [HttpGet]
    public IActionResult GetAll()
    {
        Task<List<Reservation>> reservations = _service.FindAll();

        return Ok(new { data = reservations });
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] string id)
    {
        Task<Reservation?> reservation = _service.FindById(id);

        return Ok(new { data = reservation });
    }

    [HttpPost]
    public async Task<IActionResult> NewReservation([FromBody] ReservationDto dto)
    {
        var resv = new Reservation
        {
            Id = Guid.NewGuid().ToString()[..5],
            NumberPeople = dto.NumberOfPeople,
            Date = dto.Date,
            EmailOfClient = dto.Email,
            ReservationName = dto.ReservationName,
        };

        string key = $"reservation:{resv.Id}";
        Console.WriteLine(key);
        TimeSpan expiration = TimeSpan.FromHours(1);

        await _cache.SetAsync(key, resv, expiration);
        await _mail.MakeConfirmEmail(resv);

        return Ok("Reserva pendente, clique no link do email para confirmar a reserva");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation([FromBody] ReservationDto reservation, [FromRoute] string id)
    {
        Task<Reservation> updatedReservation = _service.Update(id, reservation);

        return Ok(new { reservation = updatedReservation });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation([FromRoute] string id)
    {
        await _service.Delete(id);

        return Ok();
    }

}
