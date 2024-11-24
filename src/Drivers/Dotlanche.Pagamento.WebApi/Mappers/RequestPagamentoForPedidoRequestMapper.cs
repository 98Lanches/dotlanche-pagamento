using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.WebApi.DTOs;

namespace Dotlanche.Pagamento.WebApi.Mappers
{
    public static class RequestPagamentoForPedidoRequestMapper
    {
        public static RegistroPagamento ToDomainModel(this RequestPagamentoForPedido request)
        {
            var domainModel = new RegistroPagamento(request.IdPedido, request.Amount, request.Type);
            return domainModel;
        }
    }
}
