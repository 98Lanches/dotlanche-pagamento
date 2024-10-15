using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Exceptions;
using Dotlanche.Pagamento.Domain.ValueObjects;
using FluentAssertions;

namespace Dotlanche.Pagamento.UnitTests.Core.Domain.Entities
{
    public class RegistroPagamentoTests
    {
        [Test]
        public void New_WhenNoValidationErrorOccurs_ShouldCreateObjectWithCorrectValues()
        {
            // Arrange
            var refPedido = Guid.NewGuid();
            const decimal amount = 10;

            // Act
            var pagamento = new RegistroPagamento(refPedido, amount, TipoPagamento.QrCode);

            // Assert
            pagamento.Id.Should().NotBeEmpty();
            pagamento.IdPedido.Should().Be(refPedido);
            pagamento.Tipo.Should().Be(TipoPagamento.QrCode);
            pagamento.Amount.Should().Be(amount);
            pagamento.IsAccepted.Should().BeFalse();
            pagamento.RegisteredAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            pagamento.AcceptedAt.Should().BeNull();
        }

        [Test]
        public void New_WhenAmountIsLowerThanZero_ShouldThrowDomainValidationException()
        {
            // Arrange
            var refPedido = Guid.NewGuid();
            const decimal amount = -5;

            // Act
            var newPagamentoCall = () => new RegistroPagamento(refPedido, amount, TipoPagamento.QrCode);

            // Assert
            newPagamentoCall
                .Should()
                .Throw<DomainValidationException>()
                .WithMessage("invalid value for Amount!");
        }

        [Test]
        public void New_WhenPedidoRefIsEmpty_ShouldThrowDomainValidationException()
        {
            // Arrange
            var refPedido = Guid.Empty;
            const decimal amount = 6;

            // Act
            var newPagamentoCall = () => new RegistroPagamento(refPedido, amount, TipoPagamento.QrCode);

            // Assert
            newPagamentoCall
                .Should()
                .Throw<DomainValidationException>()
                .WithMessage("invalid value for IdPedido!");
        }

        [Test]
        public void ConfirmPayment_WhenCalled_ShouldSetIsAcceptedToTrueAndAssignAcceptedAtValue()
        {
            // Arrange
            var pagamento = new RegistroPagamento(Guid.NewGuid(), 50, TipoPagamento.QrCode);

            // Act
            pagamento.ConfirmPayment();

            // Assert
            pagamento.IsAccepted.Should().BeTrue();
            pagamento.AcceptedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}