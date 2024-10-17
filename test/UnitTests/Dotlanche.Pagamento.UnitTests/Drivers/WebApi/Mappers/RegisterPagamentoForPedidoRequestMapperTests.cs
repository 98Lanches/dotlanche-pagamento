using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.DTOs;
using Dotlanche.Pagamento.WebApi.Mappers;
using FluentAssertions;

namespace Dotlanche.Pagamento.UnitTests.Drivers.WebApi.Mappers
{
    public class RegisterPagamentoForPedidoRequestMapperTests
    {
        [Test]
        public void ToDomainModel_WhenRequestIsValid_ShouldReturnExpectedMap()
        {
            // Arrange
            var request = new RegisterPagamentoForPedidoRequest()
            {
                IdPedido = Guid.NewGuid(),
                Amount = 100,
                Type = TipoPagamento.QrCode,
            };

            // Act
            var domainModel = request.ToDomainModel();

            // Assert
            domainModel.IdPedido.Should().Be(request.IdPedido);
            domainModel.Amount.Should().Be(request.Amount);
            domainModel.Tipo.Should().Be(request.Type);
        }
    }
}