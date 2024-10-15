using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.Mappers;
using FluentAssertions;

namespace Dotlanche.Pagamento.UnitTests.Drivers.WebApi.Mappers
{
    public class RegisterPagamentoForPedidoResponseMapperTests
    {
        [Test]
        public void ToResponse_WhenResultIsValid_ShouldMapExpectedObject()
        {
            // Arrange
            var providerData = new Dictionary<string, object>()
            {
                { "TEST", "123" }
            };
            var result = new ProviderPagamentoResult(true, providerData);

            // Act
            var response = result.ToResponse();

            // Assert
            response.Result.Should().BeEquivalentTo(providerData);
        }
    }
}