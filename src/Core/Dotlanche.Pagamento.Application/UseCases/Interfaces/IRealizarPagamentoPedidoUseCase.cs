using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.Application.UseCases.Interfaces
{
    public interface IRealizarPagamentoPedidoUseCase
    {
        ProviderPagamentoResult Execute(RegistroPagamento registroPagamento);
    }
}