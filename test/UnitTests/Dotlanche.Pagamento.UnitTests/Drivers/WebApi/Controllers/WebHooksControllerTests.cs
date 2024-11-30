using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.Controllers;
using Dotlanche.Pagamento.WebApi.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dotlanche.Pagamento.UnitTests.Drivers.WebApi.Controllers
{
    public class WebHooksControllerTests
    {
        private readonly Mock<IConfirmQrCodePagamentoUseCase> mockConfirmQrCodePagamentoUseCase = new();

        public WebHooksControllerTests()
        {
            mockConfirmQrCodePagamentoUseCase.Reset();
        }

        [Test]
        public async Task ConfirmQrCodePagamento_WhenConfirmationIsSuccessful_ShouldReturnOk()
        {
            // Arrange
            var request = new ConfirmQrCodePagamentoRequest()
            {
                RegistroPagamentoId = Guid.NewGuid(),
                IsAccepted = true,
            };

            mockConfirmQrCodePagamentoUseCase.Setup(x => x.Execute(request.RegistroPagamentoId))
                .ReturnsAsync(new Result<string>(true, "pagamento accepted successfully"));

            var controller = new WebHooksController(mockConfirmQrCodePagamentoUseCase.Object);

            // Act
            var response = await controller.ConfirmQrCodePagamento(request);

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task ConfirmQrCodePagamento_WhenConfirmationIsUnsuccessful_ShouldReturnNotFound()
        {
            // Arrange
            var request = new ConfirmQrCodePagamentoRequest()
            {
                RegistroPagamentoId = Guid.NewGuid(),
                IsAccepted = true,
            };

            Result<string> result = new Result<string>(false, "Registro pagamento does not exist");
            mockConfirmQrCodePagamentoUseCase.Setup(x => x.Execute(request.RegistroPagamentoId))
                .ReturnsAsync(result);

            var controller = new WebHooksController(mockConfirmQrCodePagamentoUseCase.Object);

            // Act
            var response = await controller.ConfirmQrCodePagamento(request);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();

            var responseContent = ((NotFoundObjectResult)response).Value as string;
            responseContent.Should().NotBeNull();

            responseContent.Should().Be(result.Value);
        }
    }
}