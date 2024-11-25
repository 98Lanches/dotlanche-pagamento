namespace Dotlanche.Pagamento.Application.Ports
{
    public interface IPedidosServiceClient
    {
        Task RegisterPagamentoForPedido(Guid pedidoId, Guid registroPagamentoId);
    }
}
