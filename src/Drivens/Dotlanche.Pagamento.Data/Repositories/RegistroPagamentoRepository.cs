using Dotlanche.Pagamento.Data.DatabaseContext;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;

namespace Dotlanche.Pagamento.Data.Repositories
{
    public class RegistroPagamentoRepository : IRegistroPagamentoRepository
    {
        private readonly PagamentoDbContext dbContext;

        public RegistroPagamentoRepository(PagamentoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RegistroPagamento> AddAsync(RegistroPagamento registroPagamento)
        {
            await dbContext.Pagamentos.AddAsync(registroPagamento);
            await dbContext.SaveChangesAsync();

            return registroPagamento;
        }

        public IEnumerable<RegistroPagamento> FindByIdPedido(Guid idPedido)
        {
            return dbContext.Pagamentos.Where(x => x.IdPedido == idPedido);
        }

        public async Task<RegistroPagamento?> FindByIdAsync(Guid idRegistroPagamento)
        {
            return await dbContext.Pagamentos.FindAsync(idRegistroPagamento);
        }

        public async Task<RegistroPagamento> UpdateAsync(RegistroPagamento registroPagamento)
        {
            if (!dbContext.Pagamentos.Local.Any(e => e.Id == registroPagamento.Id))
                throw new Exception("Cannot update not tracked entity");

            dbContext.Pagamentos.Update(registroPagamento);
            await dbContext.SaveChangesAsync();

            return registroPagamento;
        }
    }
}