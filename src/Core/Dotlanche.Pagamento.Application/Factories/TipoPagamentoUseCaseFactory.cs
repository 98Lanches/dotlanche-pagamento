#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Dotlanche.Pagamento.Application.Factories
{
    public class TipoPagamentoUseCaseFactory : ITipoPagamentoUseCaseFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TipoPagamentoUseCaseFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITipoPagamentoUseCase GetUseCaseForTipoPagamento(TipoPagamento tipoPagamento)
        {
            return tipoPagamento switch
            {
                TipoPagamento.QrCode => serviceProvider.GetRequiredKeyedService<ITipoPagamentoUseCase>(TipoPagamento.QrCode),
            };
        }
    }
}