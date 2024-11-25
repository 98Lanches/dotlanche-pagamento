using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.DTOs;

namespace Dotlanche.Pagamento.WebApi.Mappers
{
    public static class RequestPagamentoForPedidoResponseMapper
    {
        public static RequestPagamentoForPedidoResponse ToResponse(this ProviderPagamentoResult result)
        {

            var response = new RequestPagamentoForPedidoResponse()
            {
                OperationSuccessful = result.IsSuccess,
                PedidoId = result.RegistroPagamento.IdPedido,
                RegistroPagamentoId = result.RegistroPagamento.Id,
                IsAccepted = result.RegistroPagamento.IsAccepted,
                RegisteredTime = result.RegistroPagamento.RegisteredAt,
                ProviderData = result.ProviderData,
            };

            return response;
        }
    }
}
