using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.Application.UseCases.Interfaces
{
    public interface IConfirmQrCodePagamentoUseCase
    {
        Task<Result<string>> Execute(Guid registroPagamentoId);
    }
}