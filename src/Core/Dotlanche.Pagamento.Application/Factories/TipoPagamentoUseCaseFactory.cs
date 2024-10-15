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
                _ => throw new ArgumentOutOfRangeException(nameof(tipoPagamento))
            };
        }
    }
}