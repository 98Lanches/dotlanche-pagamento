using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.DTOs;

namespace Dotlanche.Pagamento.WebApi.Mappers
{
    public static class RegisterPagamentoForPedidoResponseMapper
    {
        public static RegisterPagamentoForPedidoResponse ToResponse(this ProviderPagamentoResult result)
        {

            var response = new RegisterPagamentoForPedidoResponse()
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
