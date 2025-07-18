using Core.Controllers.Interfaces;
using Core.DTOs.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("[controller]")]
public class SelfService : ControllerBase
{
    private readonly ICustomerController _customerController;

    public SelfService(ICustomerController customerController)
    {
        _customerController = customerController;
    }


    [HttpGet]
    [Route("customer/{cpf}")]
    public async Task<IActionResult> GetAsync([FromRoute] string cpf, CancellationToken cancellationToken)
    {
        var response = await _customerController.GetByCpfAsync(cpf, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Route("customer")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var response = await _customerController.RegisterAsync(request, cancellationToken);

        return Ok(response);
    }
}