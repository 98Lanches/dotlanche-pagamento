using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.ValueObjects;

namespace Dotlanche.Pagamento.Application.UseCases
{
    public class PagamentoQrCodeUseCase : ITipoPagamentoUseCase
    {
        private readonly IQrCodeProvider checkoutProvider;

        public PagamentoQrCodeUseCase(IQrCodeProvider checkoutProvider)
        {
            this.checkoutProvider = checkoutProvider;
        }

        public ProviderPagamentoResult Execute(RegistroPagamento pagamento)
        {
            var qrCode = checkoutProvider.RequestQrCode(pagamento);
            var providerData = new Dictionary<string, object>()
            {
                {"QR_CODE_IMG", qrCode},
            };

            return new ProviderPagamentoResult(isSuccess: true, pagamento, providerData: providerData);
        }
    }
}