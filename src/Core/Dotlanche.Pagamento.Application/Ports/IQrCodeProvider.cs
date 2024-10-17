using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Application.Ports
{
    public interface IQrCodeProvider
    {
        string RequestQrCode(RegistroPagamento pagamento);
    }
}
