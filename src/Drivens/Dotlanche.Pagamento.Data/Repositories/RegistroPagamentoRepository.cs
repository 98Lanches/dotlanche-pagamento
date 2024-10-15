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
            return registroPagamento;
        }
    }
}
