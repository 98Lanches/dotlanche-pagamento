using System.Text.Json.Serialization;

namespace Dotlanche.Pagamento.WebApi.DTOs
{
    public class GetStatusPagamentoForPedidoResponse
    {
        public bool IsAccepted { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? AcceptedAt { get; set; }
    }
}