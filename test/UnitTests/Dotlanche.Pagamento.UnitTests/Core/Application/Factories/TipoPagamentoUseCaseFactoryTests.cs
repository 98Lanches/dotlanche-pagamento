using Dotlanche.Pagamento.Application.DependencyInjection;
using Dotlanche.Pagamento.Application.Factories;
using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Checkout.DependencyInjection;
using Dotlanche.Pagamento.Domain.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Dotlanche.Pagamento.UnitTests.Core.Application.Factories
{
    public class TipoPagamentoUseCaseFactoryTests
    {
        [Test]
        public void GetUseCaseForTipoPagamento_WhenTipoPagamentoIsQrCode_ShouldReturnPagamentoQrCodeUseCase()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddPagamentoUseCases();
            services.AddFakeCheckoutProvider();
            var factory = new TipoPagamentoUseCaseFactory(services.BuildServiceProvider());

            // Act
            var useCase = factory.GetUseCaseForTipoPagamento(TipoPagamento.QrCode);

            // Assert
            useCase.Should().BeAssignableTo<PagamentoQrCodeUseCase>();
        }
    }
}
