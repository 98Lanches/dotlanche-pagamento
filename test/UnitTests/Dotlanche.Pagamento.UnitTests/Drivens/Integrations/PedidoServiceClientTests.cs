using Dotlanche.Pagamento.Integrations.Adapters;
using FluentAssertions;
using RichardSzalay.MockHttp;
using System.Net;

namespace Dotlanche.Pagamento.UnitTests.Drivens.Integrations
{
    public class PedidoServiceClientTests
    {
        [Test]
        public async Task RegisterPagamentoForPedido_WhenResponseIsSuccess_ShouldNotThrowException()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            mockHttp
                .When(HttpMethod.Patch, "/pedido")
                .Respond(HttpStatusCode.OK);

            var httpClient = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://test.com")
            };
            var serviceClient = new PedidosServiceClient(httpClient);

            // Act
            var call = () => serviceClient.RegisterPagamentoForPedido(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            await call.Should().NotThrowAsync();
        }

        [Test]
        public async Task RegisterPagamentoForPedido_WhenResponseIsNotSuccess_ShouldThrowException()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            mockHttp
                .When(HttpMethod.Patch, "/pedido")
                .Respond(HttpStatusCode.ServiceUnavailable);

            var httpClient = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://test.com")
            };

            var serviceClient = new PedidosServiceClient(httpClient);

            // Act
            var call = () => serviceClient.RegisterPagamentoForPedido(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            await call.Should().ThrowAsync<HttpRequestException>();
        }
    }
}