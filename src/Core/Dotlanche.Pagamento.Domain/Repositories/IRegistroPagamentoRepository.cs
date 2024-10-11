using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Domain.Repositories
{
    public interface IRegistroPagamentoRepository
    {
        public RegistroPagamento Add(RegistroPagamento registroPagamento);
    }
}