using Dotlanche.Pagamento.Domain.Exceptions;

namespace Dotlanche.Pagamento.Domain.Entities
{
    public class RegistroPagamento
    {
        public Guid Id { get; private set; }

        public decimal Amount { get; }

        public bool IsAccepted { get; private set; }

        public DateTime RegisteredAt { get; private set; }

        public DateTime? AcceptedAt { get; private set; }

        public RegistroPagamento(decimal amount)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            RegisteredAt = DateTime.Now;

            ValidateEntity();
        }

        public void ConfirmPayment()
        {
            IsAccepted = true;
            AcceptedAt = DateTime.Now;
        }

        private void ValidateEntity()
        {
            if (Amount < 0)
                throw new DomainValidationException(nameof(Amount));
        }
    }
}