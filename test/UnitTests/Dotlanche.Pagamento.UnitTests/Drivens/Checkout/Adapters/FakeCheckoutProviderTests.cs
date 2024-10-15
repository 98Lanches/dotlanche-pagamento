using Dotlanche.Pagamento.Checkout.Adapters;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.ValueObjects;
using FluentAssertions;
using System.Buffers.Text;

namespace Dotlanche.Pagamento.UnitTests.Drivens.Checkout.Adapters
{
    public class FakeCheckoutProviderTests
    {
        [Test]
        public void RequestQrCode_WhenCalled_ShouldReturnValidBase64Value()
        {
            // Arrange
            var pagamento = new RegistroPagamento(Guid.NewGuid(), 35, TipoPagamento.QrCode);
            var fakeCheckoutProvider = new FakeCheckoutQrCodeProvider();

            // Act
            var qrCode = fakeCheckoutProvider.RequestQrCode(pagamento);

            // Assert
            qrCode.Should().NotBeNullOrEmpty();
            Base64.IsValid(qrCode).Should().BeTrue();
        }
    }
}