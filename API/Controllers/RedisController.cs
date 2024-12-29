using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class RedisController(RedisService service) : ControllerBase
{
    private readonly RedisService _service = service;

    [HttpGet("redis/{key}")]
    public async Task<IActionResult> Confirm([FromRoute] string key)
    {
        var res = await _service.GetAsync(key);

        return Ok(res);
    }


}
