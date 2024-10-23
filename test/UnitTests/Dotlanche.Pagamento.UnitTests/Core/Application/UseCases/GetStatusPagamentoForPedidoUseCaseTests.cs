using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Dotlanche.Pagamento.UnitTests.Core.Application.UseCases
{
    public class GetStatusPagamentoForPedidoUseCaseTests
    {
        [Test]
        public void Execute_WhenNoPaymentForPedidoIsFound_ShouldReturnFailedResult()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();

            var repositoryMock = new Mock<IRegistroPagamentoRepository>();
            repositoryMock
                .Setup(x => x.FindByIdPedido(pedidoId))
                .Returns([]);

            var useCase = new GetStatusPagamentoForPedidoUseCase(repositoryMock.Object);

            // Act
            var result = useCase.Execute(pedidoId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
        }

        [Test]
        public void Execute_WhenThereAreMultiplePagamentosAndNoneIsAccepted_ShouldReturnSuccessResultWithLast()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();

            var repositoryMock = new Mock<IRegistroPagamentoRepository>();
            IEnumerable<RegistroPagamento> pagamentoTries =
            [
                new RegistroPagamento(pedidoId, 10, TipoPagamento.QrCode),
                new RegistroPagamento(pedidoId, 10, TipoPagamento.QrCode),
                new RegistroPagamento(pedidoId, 10, TipoPagamento.QrCode),
                new RegistroPagamento(pedidoId, 10, TipoPagamento.QrCode),
                new RegistroPagamento(pedidoId, 10, TipoPagamento.QrCode),
            ];
            repositoryMock
                .Setup(x => x.FindByIdPedido(pedidoId))
                .Returns(
                pagamentoTries);

            var useCase = new GetStatusPagamentoForPedidoUseCase(repositoryMock.Object);

            // Act
            var result = useCase.Execute(pedidoId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(pagamentoTries.Last());
        }

        [Test]
        public void Execute_WhenThereIsOneAcceptedPagamento_ShouldReturnSuccessResultWithIt()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();

            var repositoryMock = new Mock<IRegistroPagamentoRepository>();

            var accepted = new RegistroPagamento(pedidoId, 10, TipoPagamento.QrCode);
            accepted.ConfirmPayment();

            repositoryMock
                .Setup(x => x.FindByIdPedido(pedidoId))
                .Returns([accepted]);

            var useCase = new GetStatusPagamentoForPedidoUseCase(repositoryMock.Object);

            // Act
            var result = useCase.Execute(pedidoId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(accepted);
        }
    }
}