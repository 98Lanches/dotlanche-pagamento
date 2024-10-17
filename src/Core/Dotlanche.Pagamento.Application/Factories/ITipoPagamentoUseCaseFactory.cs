using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.Application.Factories
{
    public interface ITipoPagamentoUseCaseFactory
    {
        ITipoPagamentoUseCase GetUseCaseForTipoPagamento(TipoPagamento tipoPagamento);
    }
}