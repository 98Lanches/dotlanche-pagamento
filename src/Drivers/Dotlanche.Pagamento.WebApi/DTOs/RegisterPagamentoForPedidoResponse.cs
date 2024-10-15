namespace Dotlanche.Pagamento.WebApi.DTOs
{
    public record RegisterPagamentoForPedidoResponse
    {
        public Dictionary<string, object>? Result { get; set; }
    }
}
