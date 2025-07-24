using Adapters.Controllers.Interfaces;
using Adapters.Presenters.Menus;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("menu")]
public class MenuApi : ControllerBase
{
    private readonly IMenuController _controller;

    public MenuApi(IMenuController controller)
    {
        _controller = controller;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetAllAsync([FromQuery] MenuItemFilter filter, CancellationToken cancellationToken)
    {
        var menuItems = await _controller.GetAllAsync(filter, cancellationToken);

        return Ok(menuItems);
    }

    [HttpGet("{id}", Name = "GetById")]
    public async Task<ActionResult<MenuItemResponse>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var menuItem = await _controller.GetByIdAsync(id, cancellationToken);

        return Ok(menuItem);
    }

    [HttpPost]
    public async Task<ActionResult<MenuItemResponse>> RegisterAsync([FromBody] RegisterMenuItemRequest request, CancellationToken cancellationToken)
    {
        var menuItem = await _controller.RegisterAsync(request, cancellationToken);

        return CreatedAtRoute("GetById", new { id = menuItem.Id }, menuItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateMenuItemRequest request, CancellationToken cancellationToken)
    {
        await _controller.UpdateAsync(id, request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _controller.SoftDeleteAsync(id, cancellationToken);

        return NoContent();
    }
}