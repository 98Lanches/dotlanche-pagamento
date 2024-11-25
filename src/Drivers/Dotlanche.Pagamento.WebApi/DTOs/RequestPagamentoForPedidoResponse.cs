namespace Dotlanche.Pagamento.WebApi.DTOs
{
    public record RequestPagamentoForPedidoResponse
    {
        public bool OperationSuccessful { get; set; }

        public Guid RegistroPagamentoId { get; set; }

        public Guid PedidoId { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime RegisteredTime { get; set; }

        public Dictionary<string, object>? ProviderData { get; set; }
    }
}