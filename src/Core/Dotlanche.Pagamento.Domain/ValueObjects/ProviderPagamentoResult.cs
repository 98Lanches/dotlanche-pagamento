using Dotlanche.Pagamento.Domain.Entities;

namespace Dotlanche.Pagamento.Domain.ValueObjects
{
    public class ProviderPagamentoResult : Result<Dictionary<string, object>>
    {
        public ProviderPagamentoResult(bool isSuccess, RegistroPagamento registroPagamento, Dictionary<string, object> providerData)
            : base(isSuccess, providerData)
        {
            RegistroPagamento = registroPagamento;
        }

        public RegistroPagamento RegistroPagamento { get; }

        public Dictionary<string, object> ProviderData => base.Value;

        public static ProviderPagamentoResult CreateFailedResult(RegistroPagamento registroPagamento, string message)
        {
            return new(false, registroPagamento, new Dictionary<string, object>()
            {
                { "Message", message },
            });
        }
    }
}