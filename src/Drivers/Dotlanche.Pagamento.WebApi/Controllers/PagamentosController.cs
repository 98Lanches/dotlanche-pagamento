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

        public PagamentosController(IRealizarPagamentoPedidoUseCase realizarPagamentoPedidoUseCase)
        {
            this.realizarPagamentoPedidoUseCase = realizarPagamentoPedidoUseCase;
        }

        /// <summary>
        /// Registra o pagamento de um pedido
        /// </summary>
        /// <param name="pagamento">Dados do pagamento</param>
        /// <returns>Resposta com informações sobre o pagamento realizado, dependendo do tipo</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterPagamentoForPedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterPagamentoForPedido([FromBody] RegisterPagamentoForPedidoRequest request)
        {
            var pagamento = request.ToDomainModel();

            var result = await realizarPagamentoPedidoUseCase.Execute(pagamento);
            if (result.IsSuccess == false)
            {
                return new ObjectResult(result)
                {
                    StatusCode = StatusCodes.Status502BadGateway,
                };
            }

            return Ok(result);
        }
    }
}