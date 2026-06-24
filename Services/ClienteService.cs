using MentoriaDDD.Dtos;
using MentoriaDDD.Models;
using MentoriaDDD.Repositories.Interfaces;
using MentoriaDDD.Validador;

namespace MentoriaDDD.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly CriarClienteValidador _criarClienteValidador;
    private readonly AtualizarClienteValidador _atualizarClienteValidador;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
        _criarClienteValidador = new CriarClienteValidador();
        _atualizarClienteValidador = new AtualizarClienteValidador();
    }

    public async Task<IReadOnlyCollection<ClienteResponse>> ObterTodosAsync()
    {
        var clientes = await _clienteRepository.ObterTodosAsync();
        return clientes.Select(MapearParaResponse).ToList();
    }

    public async Task<ClienteResponse?> ObterPorIdAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);

        return cliente is null ? null : MapearParaResponse(cliente);
    }

    public async Task<ClienteResponse?> ObterPorCPFAsync(string cpf)
    {
        var cliente = await _clienteRepository.ObterPorCpfAsync(cpf);

        return cliente is null ? null : MapearParaResponse(cliente);
    }

    public async Task<Resultado<ClienteResponse>> CriarAsync(CriarClienteRequest request)
    {
        var erroValidacao = _criarClienteValidador.Validate(request);

        if (!erroValidacao.IsValid)
        {
            return Resultado<ClienteResponse>.Falha(erroValidacao.Errors.First().ErrorMessage);
        }

        var clienteComMesmoEmail = await _clienteRepository.ObterPorEmailAsync(request.Email);

        if (clienteComMesmoEmail is not null)
        {
            return Resultado<ClienteResponse>.Falha("Ja existe um cliente com este e-mail.");
        }

        var clienteComMesmoCPF = await _clienteRepository.ObterPorCpfAsync(request.CPF);

        if (clienteComMesmoCPF is not null)
        {
            return Resultado<ClienteResponse>.Falha("Ja existe um cliente com este CPF.");
        }

        var cliente = new Cliente(request.Nome.Trim(), request.Email.Trim(), request.CPF.Trim());

        await _clienteRepository.AdicionarAsync(cliente);

        return Resultado<ClienteResponse>.Ok(MapearParaResponse(cliente));
    }

    public async Task<Resultado<ClienteResponse>> AtualizarAsync(
        int id,
        AtualizarClienteRequest request
    )
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);

        if (cliente is null)
        {
            return Resultado<ClienteResponse>.Falha("Cliente nao encontrado.");
        }

        var erroValidacao = _atualizarClienteValidador.Validate(request);

        if (!erroValidacao.IsValid)
        {
            return Resultado<ClienteResponse>.Falha(erroValidacao.Errors.First().ErrorMessage);
        }

        var clienteComMesmoEmail = await _clienteRepository.ObterPorEmailAsync(request.Email);

        if (clienteComMesmoEmail != null && clienteComMesmoEmail.Id != id)
        {
            return Resultado<ClienteResponse>.Falha("Ja existe um cliente com este e-mail.");
        }

        var clienteComMesmoCPF = await _clienteRepository.ObterPorCpfAsync(request.CPF);

        if (clienteComMesmoCPF != null && clienteComMesmoCPF.Id != id)
        {
            return Resultado<ClienteResponse>.Falha("Ja existe um cliente com este CPF.");
        }

        cliente.AtualizarDados(request.Nome.Trim(), request.Email.Trim(), request.CPF.Trim());

        await _clienteRepository.AtualizarAsync(cliente);

        return Resultado<ClienteResponse>.Ok(MapearParaResponse(cliente));
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);

        if (cliente is null)
        {
            return false;
        }

        await _clienteRepository.RemoverAsync(cliente);
        return true;
    }

    private static ClienteResponse MapearParaResponse(Cliente cliente)
    {
        return new ClienteResponse(
            cliente.Id,
            cliente.Nome,
            cliente.Email,
            cliente.CriadoEm,
            cliente.Cpf
        );
    }
}
