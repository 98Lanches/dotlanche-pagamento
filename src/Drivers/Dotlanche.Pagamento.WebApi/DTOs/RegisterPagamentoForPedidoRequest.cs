using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.WebApi.DTOs
{
    public record RegisterPagamentoForPedidoRequest
    {
        public Guid IdPedido { get; set; }

        public decimal Amount { get; set; }

        public TipoPagamento Type { get; set; }
    }
}