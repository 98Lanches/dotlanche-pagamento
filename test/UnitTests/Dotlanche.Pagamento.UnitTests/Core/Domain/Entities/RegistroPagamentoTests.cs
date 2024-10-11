using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.Exceptions;
using FluentAssertions;

namespace Dotlanche.Pagamento.UnitTests.Core.Domain.Entities
{
    public class RegistroPagamentoTests
    {
        [Test]
        public void New_WhenAmountIsLowerThanZero_ShouldThrowDomainValidationException()
        {
            // Arrange
            const decimal amount = -5;

            // Act
            var newPagamentoCall = () => new RegistroPagamento(amount);

            // Assert
            newPagamentoCall
                .Should()
                .Throw<DomainValidationException>()
                .WithMessage("invalid value for Amount!");
        }

        [Test]
        public void New_WhenNoValidationErrorOccurs_ShouldCreateObjectWithCorrectValues()
        {
            // Arrange
            const decimal amount = 10;

            // Act
            var pagamento = new RegistroPagamento(amount);

            // Assert
            pagamento.Id.Should().NotBeEmpty();
            pagamento.Amount.Should().Be(amount);
            pagamento.IsAccepted.Should().BeFalse();
            pagamento.RegisteredAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            pagamento.AcceptedAt.Should().BeNull();
        }

        [Test]
        public void ConfirmPayment_WhenCalled_ShouldSetIsAcceptedToTrueAndAssignAcceptedAtValue()
        {
            // Arrange
            var pagamento = new RegistroPagamento(amount: 50);

            // Act
            pagamento.ConfirmPayment();

            // Assert
            pagamento.IsAccepted.Should().BeTrue();
            pagamento.AcceptedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}