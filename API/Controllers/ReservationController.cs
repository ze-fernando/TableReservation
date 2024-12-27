using Microsoft.AspNetCore.Mvc;
using Services;
using Dtos;
using Models;

namespace Controllers;

[ApiController]
[Route("api")]
public class ReservationController(ReservationServices service) : ControllerBase
{

    private readonly ReservationServices _service = service;

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
    public IActionResult NewReservation([FromBody] ReservationDto dto)
    {
        Task<Reservation> newReservation = _service.Create(dto);

        return Created("", new { reservation = newReservation });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation([FromBody] ReservationDto dto, [FromRoute] string id)
    {
        Task<Reservation> updatedReservation = _service.Update(id, dto);

        return Ok(new { reservation = updatedReservation });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation([FromRoute] string id)
    {
        await _service.Delete(id);

        return Ok();
    }

    public IActionResult ConfirmReservation(string id)
    {
        //TODO
        return Ok("Confirm");
    }


}
