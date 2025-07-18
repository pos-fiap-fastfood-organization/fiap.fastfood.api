using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("[controller]")]
public class Kitchen : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Kitchen");
    }
}