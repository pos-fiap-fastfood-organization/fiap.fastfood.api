namespace Adapters.Presenters.Customers;

public class RegisterCustomerRequest
{
    public string? CPF { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}