using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.Application.UseCases
{
    public interface ITipoPagamentoUseCase
    {
        public ProviderPagamentoResult Execute(RegistroPagamento registroPagamento);
    }
}