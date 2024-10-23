namespace Dotlanche.Pagamento.WebApi.DTOs
{
    public class ConfirmQrCodePagamentoRequest
    {
        public Guid RegistroPagamentoId { get; set; }

        public bool IsAccepted { get; set; }
    }
}
