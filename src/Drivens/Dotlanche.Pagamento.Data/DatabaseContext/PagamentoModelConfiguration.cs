using Dotlanche.Pagamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotlanche.Pagamento.Data.DatabaseContext
{
    internal class PagamentoModelConfiguration : IEntityTypeConfiguration<RegistroPagamento>
    {
        public void Configure(EntityTypeBuilder<RegistroPagamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdPedido).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Tipo).IsRequired();
            builder.Property(x => x.RegisteredAt).IsRequired();
            builder.Property(x => x.IsAccepted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.AcceptedAt);
        }
    }
}