using Core.Gateways.Interfaces;
using Core.UseCases;
using NSubstitute;

namespace Core.Tests.UseCaseTests.CustomerUseCaseTests;

public class CustomerUseCaseTest
{
    public readonly ICustomerGateway _customerGateway;
    public readonly CustomerUseCase _sut;

    public CustomerUseCaseTest()
    {
        _customerGateway = Substitute.For<ICustomerGateway>();

        _sut = new CustomerUseCase(_customerGateway);
    }
}
