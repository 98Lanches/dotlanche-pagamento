using Dotlanche.Pagamento.BDDTests.Setup;
using Dotlanche.Pagamento.Data.DatabaseContext;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Dotlanche.Pagamento.WebApi.DTOs;
using Dotlanche.Pagamento.WebApi.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dotlanche.Pagamento.BDDTests.StepDefinitions
{
    [Binding]
    public sealed class PagamentoQrCodeStepDefinitions : IDisposable
    {
        private readonly HttpClient pagamentoApiClient;
        private readonly IServiceScope scope;
        private readonly PagamentoDbContext pagamentoDbContext;

        private readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

        private RegisterPagamentoForPedidoRequest request = new();
        private RegisterPagamentoForPedidoResponse? response;

        public PagamentoQrCodeStepDefinitions(PagamentoApi pagamentoApi)
        {
            pagamentoApiClient = pagamentoApi.CreateClient();
            scope = pagamentoApi.Services.CreateScope();

            pagamentoDbContext = scope.ServiceProvider.GetRequiredService<PagamentoDbContext>();
        }

        [Given("um pedido com id (.*)")]
        public void GivenAPagamentoRequestForPedidoWithId(Guid pedidoId)
        {
            request.IdPedido = pedidoId;
        }

        [Given(@"com valor de (.*)")]
        public void GivenRequestPagamentoAmountIs(decimal amount)
        {
            request.Amount = amount;
        }

        [Given(@"com tipo de pagamento QRCode")]
        public void GivenTheTipoPagamentoIsQrCode()
        {
            request.Type = TipoPagamento.QrCode;
        }

        [Given(@"que consta como pago")]
        public void GivenPedidoIsAlreadyPaid()
        {
            var existingPagamento = request.ToDomainModel();
            existingPagamento.ConfirmPayment();
            pagamentoDbContext.Add(existingPagamento);
            pagamentoDbContext.SaveChanges();
        }

        [When(@"a solicitação de pagamento para o pedido for enviada")]
        public async Task WhenPagamentoRequestIsSent()
        {
            const string endpoint = "api/pagamentos";
            var registerPagamentoHttpResponse = await pagamentoApiClient.PostAsJsonAsync(endpoint, request);

            var responseJson = await registerPagamentoHttpResponse.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<RegisterPagamentoForPedidoResponse>(responseJson, jsonOptions);
        }

        [Then(@"deve retornar um QR Code para pagamento do pedido")]
        public void ThenShouldReturnResponseWithPagamentoInfoAndQrCode()
        {
            response!.PedidoId.Should().Be(request.IdPedido);
            response.ProviderData.Should().NotBeNull();
            response!.OperationSuccessful.Should().BeTrue();
            response.ProviderData!.Should().ContainKey("QR_CODE_IMG");
        }

        [Then(@"deve retornar o id do pagamento")]
        public void ThenShouldReturnRegistroPagamentoId()
        {
            response!.RegistroPagamentoId.Should().NotBeEmpty();
        }

        [Then(@"deve falhar a operação para o pedido")]
        public void ThenShouldReturnMessageInformingPedidoIsAlreadyPaid()
        {
            response!.PedidoId.Should().Be(request.IdPedido);
            response!.OperationSuccessful.Should().BeFalse();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}