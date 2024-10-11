using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Application.Ports
{
    public interface ICheckoutProvider
    {
        string RequestQrCode(RegistroPagamento pagamento);
    }
}
