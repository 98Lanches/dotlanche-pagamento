using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Domain.Repositories
{
    public interface IRegistroPagamentoRepository
    {
        Task<RegistroPagamento> AddAsync(RegistroPagamento registroPagamento);

        IEnumerable<RegistroPagamento> FindByIdPedido(Guid idPedido);

        Task<RegistroPagamento?> FindByIdAsync(Guid idRegistroPagamento);

        Task<RegistroPagamento> UpdateAsync(RegistroPagamento registroPagamento);
    }
}