using Core.Controllers.Interfaces;
using Core.DTOs.Customers;
using Core.Entities;
using Core.UseCases.Interfaces;

namespace Core.Controllers;

public class CustomerController : ICustomerController
{
    private readonly ICustomerUseCase _customerUseCase;

    public CustomerController(ICustomerUseCase customerUseCase)
    {
        _customerUseCase = customerUseCase;
    }

    public async Task<CustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var customer = await _customerUseCase.GetByCpfAsync(cpf, cancellationToken);

        var response = new CustomerResponse(customer.Id!, customer.Email ?? string.Empty);

        return response;
    }

    public async Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.CPF, request.Name, request.Email);

        customer = await _customerUseCase.InsertOneAsync(customer, cancellationToken);

        var response = new CustomerResponse(customer.Id!, customer.Email ?? string.Empty);

        return response;
    }
}