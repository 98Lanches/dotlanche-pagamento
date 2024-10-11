using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Dotlanche.Pagamento.Application.UseCases
{
    public class RealizarPagamentoPedidoUseCase : IRealizarPagamentoPedidoUseCase
    {
        private readonly IRegistroPagamentoRepository repository;
        private readonly IServiceProvider serviceProvider;

        public RealizarPagamentoPedidoUseCase(IRegistroPagamentoRepository repository, IServiceProvider serviceProvider)
        {
            this.repository = repository;
            this.serviceProvider = serviceProvider;
        }

        public ProviderPagamentoResult Execute(RegistroPagamento registroPagamento)
        {
            var useCaseForTipoPagamento = GetUseCaseForTipoPagamento(registroPagamento.Tipo);

            var result = useCaseForTipoPagamento.Execute(registroPagamento);
            repository.Add(registroPagamento);

            return result;
        }

        private ITipoPagamentoUseCase GetUseCaseForTipoPagamento(TipoPagamento tipoPagamento)
        {
            return tipoPagamento switch
            {
                TipoPagamento.QrCode => serviceProvider.GetRequiredKeyedService<ITipoPagamentoUseCase>(TipoPagamento.QrCode),
                _ => throw new ArgumentOutOfRangeException(nameof(tipoPagamento))
            };
        }
    }
}