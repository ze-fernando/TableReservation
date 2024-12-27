using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Controllers;

public class ReservationController : ControllerBase
{
    public IActionResult GetAll()
    {
        return Ok("All");
    }

    public IActionResult GetById(int id)
    {
        return Ok("By id");
    }

    public IActionResult NewReservation(ReservationDto dto)
    {
        return Ok("New");
    }

    public IActionResult UpdateReservation(ReservationDto dto, int id)
    {
        return Ok("Update");
    }

    public IActionResult DeleteReservation(int id)
    {
        return Ok("Delete");
    }

    public IActionResult ConfirmReservation(int id)
    {
        return Ok("Confirm");
    }


}
