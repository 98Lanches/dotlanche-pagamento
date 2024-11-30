using AutoBogus;
using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.Domain.Entities;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.Controllers;
using Dotlanche.Pagamento.WebApi.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dotlanche.Pagamento.UnitTests.Drivers.WebApi.Controllers
{
    public class PagamentosControllerTests
    {
        private readonly Mock<ISolicitarPagamentoPedidoUseCase> mockSolicitarPagamentoPedidoUseCase = new();
        private readonly Mock<IGetStatusPagamentoForPedidoUseCase> mockGetStatusPedidoUseCase = new();

        public PagamentosControllerTests()
        {
            mockSolicitarPagamentoPedidoUseCase.Reset();
            mockGetStatusPedidoUseCase.Reset();
        }

        [Test]
        public void GetStatusPagamentoForPedido_WhenPagamentoExists_Should200OkWithRegistroPagamentoData()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();
            var existingPagamento = new AutoFaker<RegistroPagamento>()
                .RuleFor(x => x.IdPedido, pedidoId)
                .Generate();
            var result = new Result<RegistroPagamento?>(true, existingPagamento);

            mockGetStatusPedidoUseCase
                .Setup(x => x.Execute(pedidoId))
                .Returns(result);

            var controller = new PagamentosController(mockSolicitarPagamentoPedidoUseCase.Object,
                                                      mockGetStatusPedidoUseCase.Object);

            // Act
            var response = controller.GetStatusPagamentoForPedido(pedidoId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            var responseData = ((OkObjectResult)response.Result!).Value as GetStatusPagamentoForPedidoResponse;
            responseData.Should().NotBeNull();

            responseData!.RegistroPagamentoId.Should().Be(existingPagamento.Id);
            responseData!.PedidoId.Should().Be(pedidoId);
            responseData!.IsAccepted.Should().Be(existingPagamento.IsAccepted);
            responseData!.AcceptedAt.Should().Be(existingPagamento.AcceptedAt);
        }

        [Test]
        public void GetStatusPagamentoForPedido_WhenPagamentoDoesNotExist_Should404()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();
            var result = new Result<RegistroPagamento?>(false, null);

            mockGetStatusPedidoUseCase
                .Setup(x => x.Execute(pedidoId))
                .Returns(result);

            var controller = new PagamentosController(mockSolicitarPagamentoPedidoUseCase.Object,
                                                      mockGetStatusPedidoUseCase.Object);

            // Act
            var response = controller.GetStatusPagamentoForPedido(pedidoId);

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
            var responseContent = ((NotFoundObjectResult)response.Result!).Value as string;
            responseContent.Should().NotBeNull();

            responseContent.Should().Be("Pagamento not found");
        }
    }
}