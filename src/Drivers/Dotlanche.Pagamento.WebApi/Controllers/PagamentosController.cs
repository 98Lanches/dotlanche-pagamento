using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.WebApi.DTOs;
using Dotlanche.Pagamento.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Dotlanche.Pagamento.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly IRealizarPagamentoPedidoUseCase realizarPagamentoPedidoUseCase;
        private readonly IGetStatusPagamentoForPedidoUseCase getStatusPagamentoForPedidoUseCase;

        public PagamentosController(
            IRealizarPagamentoPedidoUseCase realizarPagamentoPedidoUseCase,
            IGetStatusPagamentoForPedidoUseCase getStatusPagamentoForPedidoUseCase)
        {
            this.realizarPagamentoPedidoUseCase = realizarPagamentoPedidoUseCase;
            this.getStatusPagamentoForPedidoUseCase = getStatusPagamentoForPedidoUseCase;
        }

        /// <summary>
        /// Registra o pagamento de um pedido
        /// </summary>
        /// <param name="pagamento">Dados do pagamento</param>
        /// <returns>Resposta com informações sobre o pagamento realizado, dependendo do tipo</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterPagamentoForPedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterPagamentoForPedidoResponse), StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> RegisterPagamentoForPedido([FromBody] RegisterPagamentoForPedidoRequest request)
        {
            var pagamento = request.ToDomainModel();

            var result = await realizarPagamentoPedidoUseCase.Execute(pagamento);
            if (result.IsSuccess == false)
            {
                return new ObjectResult(result.ToResponse())
                {
                    StatusCode = StatusCodes.Status502BadGateway,
                };
            }

            return Ok(result.ToResponse());
        }

        /// <summary>
        /// Obtém o status de um pagamento para um pedido
        /// </summary>
        /// <param name="pagamento">id pedido</param>
        /// <returns>Status do pedido, se foi pago ou não</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetStatusPagamentoForPedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStatusPagamentoForPedido([FromQuery] Guid idPedido)
        {
            var result = getStatusPagamentoForPedidoUseCase.Execute(idPedido);
            if (!result.IsSuccess)
                return NotFound();

            var response = new GetStatusPagamentoForPedidoResponse()
            {
                IsAccepted = result.Value!.IsAccepted,
                AcceptedAt = result.Value!.AcceptedAt
            };
            return Ok(response);
        }
    }
}