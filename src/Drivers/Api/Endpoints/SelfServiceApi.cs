using Adapters.Controllers.Interfaces;
using Adapters.DTOs.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("selfservice")]
public class SelfServiceApi : ControllerBase
{
    private readonly ICustomerController _customerController;

    public SelfServiceApi(ICustomerController customerController)
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