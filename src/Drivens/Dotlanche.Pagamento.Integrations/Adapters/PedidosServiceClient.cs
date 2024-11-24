using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Integrations.DTOs;
using System.Net.Http.Json;

namespace Dotlanche.Pagamento.Integrations.Adapters
{
    internal class PedidosServiceClient : IPedidosServiceClient
    {
        private readonly HttpClient client;

        public PedidosServiceClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task RegisterPagamentoForPedido(Guid pedidoId, Guid registroPagamentoId)
        {
            var request = new RegisterPagamentoForPedidoRequest()
            {
                PedidoId = pedidoId,
                RegistroPagamentoId = registroPagamentoId
            };
            var response = await client.PatchAsJsonAsync("/pedido", request);

            response.EnsureSuccessStatusCode();
        }
    }
}