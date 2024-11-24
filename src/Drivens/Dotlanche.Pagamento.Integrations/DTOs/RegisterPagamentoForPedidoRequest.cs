namespace Dotlanche.Pagamento.Integrations.DTOs
{
    internal class RegisterPagamentoForPedidoRequest
    {
        public Guid PedidoId { get; set; }
        public Guid RegistroPagamentoId { get; set; }
    }
}