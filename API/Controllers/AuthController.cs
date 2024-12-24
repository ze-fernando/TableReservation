using Microsoft.AspNetCore.Mvc;
using Dtos;


namespace Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    public IActionResult Login(LoginDto dto)
    {
        return Ok("Login");
    }

    public IActionResult Signup(UserDto dto)
    {
        return Ok("Signup");
    }
}
