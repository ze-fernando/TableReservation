using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Dtos;
using API.Models;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class ReservationController(ReservationService service, EmailService mailService) : ControllerBase
{

    private readonly ReservationService _service = service;
    private readonly EmailService _mailService = mailService;

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
        Reservation newReservation = await _service.Create(dto);

        await _mailService.MakeConfirmEmail(newReservation);

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

    [HttpGet("confirm-reservation/{id}")]
    public async Task<IActionResult> ConfirmReservation([FromRoute] string id)
    {
        var result = await _service.ConfirmResv(id);

        if (!result)
            return BadRequest("Reservation invalid or already confirm");

        return Ok("Confirmed");
    }


}
