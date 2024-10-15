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
                Result = result.ProviderData,
            };

            return response;
        }
    }
}
