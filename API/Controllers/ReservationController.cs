using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Controllers;

public class ReservationController : ControllerBase
{
    public IActionResult GetAll()
    {
        ICollection<Reservation> reservations = _service.FindAll();

        return Ok(new { data = reservations });
    }

    public IActionResult GetById(int id)
    {
        Reservation reservation = _service.FindById(id);

        return Ok(new { data = reservation });
    }

    public IActionResult NewReservation(ReservationDto dto)
    {
        Reservation newReservation = _service.Create(dto);

        return Created(new { reservation = newReservation });
    }

    public IActionResult UpdateReservation(ReservationDto dto, int id)
    {
        Reservation updatedReservation = _service.Update(id, dto);

        return Created(new { reservation = updatedReservation });
    }

    public IActionResult DeleteReservation(int id)
    {
        _service.Delete(id);

        return Deleted();
    }

    public IActionResult ConfirmReservation(int id)
    {
        //TODO
        return Ok("Confirm");
    }


}
