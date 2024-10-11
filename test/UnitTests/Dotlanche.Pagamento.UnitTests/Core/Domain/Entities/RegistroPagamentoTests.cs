using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Exceptions;
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
            var pagamento = new RegistroPagamento(refPedido, amount);

            // Assert
            pagamento.Id.Should().NotBeEmpty();
            pagamento.Pedido.Should().Be(refPedido);
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
            var newPagamentoCall = () => new RegistroPagamento(refPedido, amount);

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
            var newPagamentoCall = () => new RegistroPagamento(refPedido, amount);

            // Assert
            newPagamentoCall
                .Should()
                .Throw<DomainValidationException>()
                .WithMessage("invalid value for PedidoRef!");
        }

        [Test]
        public void ConfirmPayment_WhenCalled_ShouldSetIsAcceptedToTrueAndAssignAcceptedAtValue()
        {
            // Arrange
            var pagamento = new RegistroPagamento(Guid.NewGuid(), 50);

            // Act
            pagamento.ConfirmPayment();

            // Assert
            pagamento.IsAccepted.Should().BeTrue();
            pagamento.AcceptedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}