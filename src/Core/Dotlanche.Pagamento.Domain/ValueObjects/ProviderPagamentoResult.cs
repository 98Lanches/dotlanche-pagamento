namespace Dotlanche.Pagamento.Domain.ValueObjects
{
    public class ProviderPagamentoResult
    {
        public ProviderPagamentoResult(bool isSuccess, Dictionary<string, object> providerData)
        {
            IsSuccess = isSuccess;
            ProviderData = providerData;
        }

        public bool IsSuccess { get; }

        public Dictionary<string, object> ProviderData { get; }
    }
}