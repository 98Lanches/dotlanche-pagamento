using System.Text.Json.Serialization;

namespace Dotlanche.Pagamento.WebApi.DTOs
{
    public class GetStatusPagamentoForPedidoResponse
    {
        public Guid RegistroPagamentoId { get; set; }
        public Guid PedidoId { get; set; }
        public bool IsAccepted { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? AcceptedAt { get; set; }
    }
}