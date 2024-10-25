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

            var repositoryMock = new Mock<IRegistroPagamentoRepository>();
            repositoryMock
                .Setup(x => x.FindByIdAsync(paymentId))
                .ReturnsAsync(new RegistroPagamento(paymentId, 10, TipoPagamento.QrCode));

            var useCase = new ConfirmQrCodePagamentoUseCase(repositoryMock.Object);

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

            var useCase = new ConfirmQrCodePagamentoUseCase(repositoryMock.Object);

            // Act
            var result = await useCase.Execute(paymentId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().Be("Registro pagamento does not exist");
        }
    }
}