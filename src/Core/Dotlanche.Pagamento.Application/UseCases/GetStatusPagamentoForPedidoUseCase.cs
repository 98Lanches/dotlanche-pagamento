using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.Application.UseCases
{
    public class GetStatusPagamentoForPedidoUseCase : IGetStatusPagamentoForPedidoUseCase
    {
        private readonly IRegistroPagamentoRepository registroPagamentoRepository;

        public GetStatusPagamentoForPedidoUseCase(IRegistroPagamentoRepository registroPagamentoRepository)
        {
            this.registroPagamentoRepository = registroPagamentoRepository;
        }

        public Result<RegistroPagamento?> Execute(Guid idPedido)
        {
            var pagamentos = registroPagamentoRepository.FindByIdPedido(idPedido);
            if (!pagamentos.Any())
                return new Result<RegistroPagamento?>(false, null);

            var isAccepted = pagamentos.Any(x => x.IsAccepted);
            return isAccepted
                ? new Result<RegistroPagamento?>(true, pagamentos.First(x => x.IsAccepted))
                : new Result<RegistroPagamento?>(true, pagamentos.OrderBy(x => x.RegisteredAt).Last());
        }
    }
}
