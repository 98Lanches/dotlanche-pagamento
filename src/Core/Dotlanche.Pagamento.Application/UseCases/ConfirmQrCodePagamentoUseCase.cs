using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.Application.UseCases
{
    public class ConfirmQrCodePagamentoUseCase : IConfirmQrCodePagamentoUseCase
    {
        private readonly IRegistroPagamentoRepository registroPagamentoRepository;
        private readonly IPedidosServiceClient pedidosClient;

        public ConfirmQrCodePagamentoUseCase(IRegistroPagamentoRepository registroPagamentoRepository, IPedidosServiceClient pedidosClient)
        {
            this.registroPagamentoRepository = registroPagamentoRepository;
            this.pedidosClient = pedidosClient;
        }

        public async Task<Result<string>> Execute(Guid registroPagamentoId)
        {
            var registroPgto = await registroPagamentoRepository.FindByIdAsync(registroPagamentoId);
            if (registroPgto == null)
                return new Result<string>(false, "Registro pagamento does not exist");

            registroPgto.ConfirmPayment();

            await pedidosClient.RegisterPagamentoForPedido(registroPgto.IdPedido, registroPgto.Id);
            await registroPagamentoRepository.UpdateAsync(registroPgto);

            return new Result<string>(true, "pagamento accepted successfully");
        }
    }
}