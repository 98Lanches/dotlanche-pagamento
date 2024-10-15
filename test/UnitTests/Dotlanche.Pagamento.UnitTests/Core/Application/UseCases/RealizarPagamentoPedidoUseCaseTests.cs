using Dotlanche.Pagamento.Application.Factories;
using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Moq;

namespace Dotlanche.Pagamento.UnitTests.Core.Application.UseCases
{
    public class RealizarPagamentoPedidoUseCaseTests
    {
        [Test]
        public void Execute_WhenRegistroPagamentoIsValid_ShouldExecuteTipoPagamentoUseCaseAndAddToRepository()
        {
            // Arrange
            var payment = new RegistroPagamento(Guid.NewGuid(), 35, TipoPagamento.QrCode);

            var factoryMock = new Mock<ITipoPagamentoUseCaseFactory>();
            var repositoryMock = new Mock<IRegistroPagamentoRepository>();
            var tipoPagamentoUseCaseMock = new Mock<ITipoPagamentoUseCase>();
            var tipoPagamentoMock = factoryMock
                .Setup(x => x.GetUseCaseForTipoPagamento(TipoPagamento.QrCode))
                .Returns(tipoPagamentoUseCaseMock.Object);

            var useCase = new RealizarPagamentoPedidoUseCase(repositoryMock.Object, factoryMock.Object);

            // Act
            useCase.Execute(payment);

            // Assert
            tipoPagamentoUseCaseMock.Verify(x => x.Execute(payment), Times.Once());
            repositoryMock.Verify(x => x.Add(payment), Times.Once());
        }
    }
}