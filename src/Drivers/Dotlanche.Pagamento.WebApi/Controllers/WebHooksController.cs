using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Dotlanche.Pagamento.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHooksController : ControllerBase
    {
        private readonly IConfirmQrCodePagamentoUseCase confirmQrCodePagamentoUseCase;

        public WebHooksController(IConfirmQrCodePagamentoUseCase confirmQrCodePagamentoUseCase)
        {
            this.confirmQrCodePagamentoUseCase = confirmQrCodePagamentoUseCase;
        }

        /// <summary>
        /// Confirma o pagamento de um pedido realizado via QR Code.
        /// </summary>
        /// <param name="request">requisição com id do pagamento e confirmação de pagamento</param>
        [HttpPost("confirm-payment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmQrCodePagamento([FromBody] ConfirmQrCodePagamentoRequest request)
        {
            var result = await confirmQrCodePagamentoUseCase.Execute(request.RegistroPagamentoId);
            if (!result.IsSuccess)
                return NotFound(result.Value);

            return Ok();
        }
    }
}