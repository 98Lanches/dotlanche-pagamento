using Dotlanche.Pagamento.Application.Factories;
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
        private readonly ITipoPagamentoUseCaseFactory useCaseFactory;

        public RealizarPagamentoPedidoUseCase(IRegistroPagamentoRepository repository, ITipoPagamentoUseCaseFactory useCaseFactory)
        {
            this.repository = repository;
            this.useCaseFactory = useCaseFactory;
        }

        public ProviderPagamentoResult Execute(RegistroPagamento registroPagamento)
        {
            var useCaseForTipoPagamento =  useCaseFactory.GetUseCaseForTipoPagamento(registroPagamento.Tipo);

            var result = useCaseForTipoPagamento.Execute(registroPagamento);
            repository.AddAsync(registroPagamento);

            return result;
        }

    }
}