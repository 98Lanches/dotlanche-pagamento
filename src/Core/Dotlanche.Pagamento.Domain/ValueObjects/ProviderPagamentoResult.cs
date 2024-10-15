using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Domain.ValueObjects
{
    public class ProviderPagamentoResult
    {
        public ProviderPagamentoResult(bool isSuccess, RegistroPagamento registroPagamento, Dictionary<string, object> providerData)
        {
            IsSuccess = isSuccess;
            RegistroPagamento = registroPagamento;
            ProviderData = providerData;
        }

        public bool IsSuccess { get; }

        public RegistroPagamento RegistroPagamento { get; }

        public Dictionary<string, object> ProviderData { get; }
    }
}