using AutoBogus;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.Mappers;
using FluentAssertions;

namespace Dotlanche.Pagamento.UnitTests.Drivers.WebApi.Mappers
{
    public class RequestPagamentoForPedidoResponseMapperTests
    {
        [Test]
        public void ToResponse_WhenResultIsValid_ShouldMapExpectedObject()
        {
            // Arrange
            var providerData = new Dictionary<string, object>()
            {
                { "TEST", "123" }
            };
            var pagamento = new AutoFaker<RegistroPagamento>().Generate();
            var result = new ProviderPagamentoResult(true, pagamento, providerData);

            // Act
            var response = result.ToResponse();

            // Assert
            response.OperationSuccessful.Should().Be(result.IsSuccess);
            response.RegistroPagamentoId.Should().Be(result.RegistroPagamento.Id);
            response.PedidoId.Should().Be(result.RegistroPagamento.IdPedido);
            response.RegisteredTime.Should().Be(result.RegistroPagamento.RegisteredAt);
            response.IsAccepted.Should().Be(result.RegistroPagamento.IsAccepted);
            response.ProviderData.Should().BeEquivalentTo(providerData);
        }
    }
}