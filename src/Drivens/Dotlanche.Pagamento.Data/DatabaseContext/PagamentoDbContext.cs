using Dotlanche.Pagamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotlanche.Pagamento.Data.DatabaseContext
{
    public class PagamentoDbContext : DbContext
    {
        public DbSet<RegistroPagamento> Pagamentos { get; set; }

        public PagamentoDbContext(DbContextOptions<PagamentoDbContext> options)
            :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PagamentoModelConfiguration().Configure(modelBuilder.Entity<RegistroPagamento>());
        }
    }
}
