using Dotlanche.Pagamento.Checkout.Adapters;
using Dotlanche.Pagamento.Domain.Entities;
using FluentAssertions;

namespace Dotlanche.Pagamento.UnitTests.Drivens.Checkout.Adapters
{
    public class FakeCheckoutProviderTests
    {
        [Test]
        public void RequestQrCode_WhenCalled_ShouldReturnFakeValue()
        {
            // Arrange
            var pagamento = new RegistroPagamento(35);
            var fakeCheckoutProvider = new FakeCheckoutProvider();

            // Act
            var qrCode = fakeCheckoutProvider.RequestQrCode(pagamento);

            // Assert
            qrCode.Should().Be("DecodedQRCode");
        }
    }
}