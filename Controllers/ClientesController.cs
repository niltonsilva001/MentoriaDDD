using MentoriaDDD.Dtos;
using MentoriaDDD.Services;
using Microsoft.AspNetCore.Mvc;

namespace MentoriaDDD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            var clientes = await _clienteService.ObterTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            return cliente is null
                ? NotFound(new { mensagem = "Cliente nao encontrado." })
                : Ok(cliente);
        }

        [HttpGet("ObterPorCPF/{cpf}")]
        public async Task<IActionResult> ObterPorCPF(string cpf)
        {
            var cpfLimpo = cpf.Replace(".", "").Replace("-", "").Trim();

            if (cpfLimpo.Length != 11 || !long.TryParse(cpfLimpo, out _))
            {
                return BadRequest(new { mensagem = "CPF invalido." });
            }

            var cliente = await _clienteService.ObterPorCPFAsync(cpfLimpo);

            return cliente is null
                ? NotFound(new { mensagem = "Cliente nao encontrado." })
                : Ok(cliente);
        }

        [HttpPost("CriarCliente")]
        public async Task<IActionResult> CriarCliente(CriarClienteRequest request)
        {
            var resultado = await _clienteService.CriarAsync(request);
            if (!resultado.Sucesso)
            {
                return BadRequest(new { mensagem = resultado.Erro });
            }
            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = resultado.Valor.Id },
                resultado.Valor
            );
        }

        [HttpPut("AtualizarCliente/{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, AtualizarClienteRequest request)
        {
            var resultado = await _clienteService.AtualizarAsync(id, request);

            if (!resultado.Sucesso)
            {
                return resultado.Erro == "Cliente nao encontrado."
                    ? NotFound(new { mensagem = resultado.Erro })
                    : BadRequest(new { mensagem = resultado.Erro });
            }

            return Ok(resultado.Valor);
        }

        [HttpDelete("RemoverCliente/{id}")]
        public async Task<IActionResult> RemoverCliente(int id)
        {
            var removido = await _clienteService.RemoverAsync(id);

            return removido ? NoContent() : NotFound(new { mensagem = "Cliente nao encontrado." });
        }
    }
}
