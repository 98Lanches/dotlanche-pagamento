using Dotlanche.Pagamento.Application.Factories;
using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Dotlanche.Pagamento.UnitTests.Core.Application.UseCases
{
    public class RealizarPagamentoPedidoUseCaseTests
    {
        [Test]
        public async Task Execute_WhenRegistroPagamentoIsValid_ShouldExecuteTipoPagamentoUseCaseAndAddToRepository()
        {
            // Arrange
            var payment = new RegistroPagamento(Guid.NewGuid(), 35, TipoPagamento.QrCode);

            var factoryMock = new Mock<ITipoPagamentoUseCaseFactory>();
            var repositoryMock = new Mock<IRegistroPagamentoRepository>();
            var tipoPagamentoUseCaseMock = new Mock<ITipoPagamentoUseCase>();
            var tipoPagamentoMock = factoryMock
                .Setup(x => x.GetUseCaseForTipoPagamento(TipoPagamento.QrCode))
                .Returns(tipoPagamentoUseCaseMock.Object);

            var useCase = new RealizarPagamentoPedidoUseCase(repositoryMock.Object,
                                                             factoryMock.Object,
                                                             new Mock<ILogger<RealizarPagamentoPedidoUseCase>>().Object);

            // Act
            await useCase.Execute(payment);

            // Assert
            tipoPagamentoUseCaseMock.Verify(x => x.Execute(payment), Times.Once());
            repositoryMock.Verify(x => x.AddAsync(payment), Times.Once());
        }

        [Test]
        public async Task Execute_WhenTipoPagamentoUseCaseIsExecutedSuccessfully_ShouldReturnSuccessResult()
        {
            // Arrange
            var payment = new RegistroPagamento(Guid.NewGuid(), 35, TipoPagamento.QrCode);

            var factoryMock = new Mock<ITipoPagamentoUseCaseFactory>();
            var repositoryMock = new Mock<IRegistroPagamentoRepository>();

            var tipoPagamentoUseCaseMock = new Mock<ITipoPagamentoUseCase>();
            var sucessProviderResult = new ProviderPagamentoResult(
                true, 
                payment, 
                new Dictionary<string, object>()
                {
                    { "QR_CODE_IMG", "TET" }
                });
            tipoPagamentoUseCaseMock
                .Setup(x => x.Execute(payment))
                .Returns(sucessProviderResult);

            var tipoPagamentoMock = factoryMock
                .Setup(x => x.GetUseCaseForTipoPagamento(TipoPagamento.QrCode))
                .Returns(tipoPagamentoUseCaseMock.Object);

            var useCase = new RealizarPagamentoPedidoUseCase(repositoryMock.Object,
                                                             factoryMock.Object,
                                                             new Mock<ILogger<RealizarPagamentoPedidoUseCase>>().Object);

            // Act
            var result = await useCase.Execute(payment);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.RegistroPagamento.Should().Be(payment);
            result.ProviderData.Should().BeEquivalentTo(sucessProviderResult.ProviderData);
        }

        [Test]
        public async Task Execute_WhenTipoPagamentoUseCaseFails_ShouldReturnFailedResult()
        {
            // Arrange
            var payment = new RegistroPagamento(Guid.NewGuid(), 35, TipoPagamento.QrCode);

            var factoryMock = new Mock<ITipoPagamentoUseCaseFactory>();
            var repositoryMock = new Mock<IRegistroPagamentoRepository>();

            var tipoPagamentoUseCaseMock = new Mock<ITipoPagamentoUseCase>();
            tipoPagamentoUseCaseMock
                .Setup(x => x.Execute(payment))
                .Throws(new Exception("An error occurred"));

            var tipoPagamentoMock = factoryMock
                .Setup(x => x.GetUseCaseForTipoPagamento(TipoPagamento.QrCode))
                .Returns(tipoPagamentoUseCaseMock.Object);

            var useCase = new RealizarPagamentoPedidoUseCase(repositoryMock.Object,
                                                             factoryMock.Object,
                                                             new Mock<ILogger<RealizarPagamentoPedidoUseCase>>().Object);

            // Act
            var result = await useCase.Execute(payment);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.RegistroPagamento.Should().Be(payment);
            result.ProviderData.Should().Contain(new KeyValuePair<string, object>("Message", "An error occurred"));
        }
    }
}