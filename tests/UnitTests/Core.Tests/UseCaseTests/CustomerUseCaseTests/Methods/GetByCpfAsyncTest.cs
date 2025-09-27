using NSubstitute;

namespace Core.Tests.UseCaseTests.CustomerUseCaseTests.Methods;

public class GetByCpfAsyncTest : CustomerUseCaseTest
{
    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("")]
    public async Task Should_Call_GetByCpfAsync_Once(string cpf)
    {
        // Act && Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
             () =>  _sut.GetByCpfAsync(cpf, CancellationToken.None));

        await _customerGateway
            .DidNotReceive()
            .GetByCpfAsync(cpf, CancellationToken.None);
    }
}
