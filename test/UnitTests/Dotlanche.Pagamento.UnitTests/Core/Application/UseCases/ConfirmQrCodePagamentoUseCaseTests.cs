using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Dotlanche.Pagamento.UnitTests.Core.Application.UseCases
{
    public class ConfirmQrCodePagamentoUseCaseTests
    {
        [Test]
        public async Task Execute_WhenPaymentExists_ShouldReturnSuccessResult()
        {
            // Arrange
            var paymentId = Guid.NewGuid();

            var registroPagamentoRepositoryMock = new Mock<IRegistroPagamentoRepository>();
            registroPagamentoRepositoryMock
                .Setup(x => x.FindByIdAsync(paymentId))
                .ReturnsAsync(new RegistroPagamento(paymentId, 10, TipoPagamento.QrCode));

            var pedidosServiceClientMock = new Mock<IPedidosServiceClient>();

            var useCase = new ConfirmQrCodePagamentoUseCase(registroPagamentoRepositoryMock.Object, pedidosServiceClientMock.Object);

            // Act
            var result = await useCase.Execute(paymentId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task Execute_WhenPaymentDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            var paymentId = Guid.NewGuid();

            var repositoryMock = new Mock<IRegistroPagamentoRepository>();

            var pedidosServiceClientMock = new Mock<IPedidosServiceClient>();

            var useCase = new ConfirmQrCodePagamentoUseCase(repositoryMock.Object, pedidosServiceClientMock.Object);

            // Act
            var result = await useCase.Execute(paymentId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().Be("Registro pagamento does not exist");
        }
    }
}