using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Checkout.Adapters
{
    public class FakeCheckoutProvider : ICheckoutProvider
    {
        public string RequestQrCode(RegistroPagamento pagamento)
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
            return "DecodedQRCode";
        }
    }
}