using Dotlanche.Pagamento.Application.Factories;
using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Dotlanche.Pagamento.Application.UseCases
{
    public class RealizarPagamentoPedidoUseCase : IRealizarPagamentoPedidoUseCase
    {
        private readonly IRegistroPagamentoRepository repository;
        private readonly ITipoPagamentoUseCaseFactory useCaseFactory;
        private readonly ILogger<RealizarPagamentoPedidoUseCase> logger;

        public RealizarPagamentoPedidoUseCase(
            IRegistroPagamentoRepository repository,
            ITipoPagamentoUseCaseFactory useCaseFactory,
            ILogger<RealizarPagamentoPedidoUseCase> logger)
        {
            this.repository = repository;
            this.useCaseFactory = useCaseFactory;
            this.logger = logger;
        }

        public async Task<ProviderPagamentoResult> Execute(RegistroPagamento registroPagamento)
        {
            var useCaseForTipoPagamento = useCaseFactory.GetUseCaseForTipoPagamento(registroPagamento.Tipo);

            try
            {
                var result = useCaseForTipoPagamento.Execute(registroPagamento);
                await repository.AddAsync(registroPagamento);

                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e, "error processing payment");
                return new ProviderPagamentoResult(false, registroPagamento, new Dictionary<string, object>()
                {
                    { "Message", e.Message }
                });
            }
        }
    }
}