using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Domain.Repositories
{
    public interface IRegistroPagamentoRepository
    {
        public Task<RegistroPagamento> AddAsync(RegistroPagamento registroPagamento);
    }
}