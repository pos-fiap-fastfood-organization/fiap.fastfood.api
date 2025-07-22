using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("kitchen")]
public class KitchenApi : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Kitchen");
    }
}