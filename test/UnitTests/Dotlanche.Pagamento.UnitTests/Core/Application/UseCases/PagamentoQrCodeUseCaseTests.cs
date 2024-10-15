using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Dotlanche.Pagamento.UnitTests.Core.Application.UseCases
{
    public class PagamentoQrCodeUseCaseTests
    {
        [Test]
        public void Execute_WhenPaymentIsValid_ShouldReturnProviderDataWithQrCodeImage()
        {
            // Arrange
            var payment = new RegistroPagamento(Guid.NewGuid(), 35, TipoPagamento.QrCode);
            var qrCodeProvider = new Mock<IQrCodeProvider>();

            const string qrCode = "aQrCode";
            qrCodeProvider.Setup(x => x.RequestQrCode(payment)).Returns(qrCode);
            var useCase = new PagamentoQrCodeUseCase(qrCodeProvider.Object);

            // Act
            var providerResult = useCase.Execute(payment);

            // Assert
            providerResult.IsSuccess.Should().BeTrue();
            providerResult.ProviderData.Should().Contain(new KeyValuePair<string, object>("QR_CODE_IMG", qrCode));
        }
    }
}