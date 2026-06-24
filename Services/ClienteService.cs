using MentoriaDDD.Dtos;
using MentoriaDDD.Models;
using MentoriaDDD.Repositories.Interfaces;

namespace MentoriaDDD.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
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
        var erroValidacao = ValidarDados(request.Nome, request.Email, request.CPF);

        if (erroValidacao is not null)
        {
            return Resultado<ClienteResponse>.Falha(erroValidacao);
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

        var erroValidacao = ValidarDados(request.Nome, request.Email, request.CPF);

        if (erroValidacao is not null)
        {
            return Resultado<ClienteResponse>.Falha(erroValidacao);
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

    private static string? ValidarDados(string nome, string email, string cpf)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return "Nome e obrigatorio.";
        }

        if (nome.Trim().Length < 3)
        {
            return "Nome deve ter pelo menos 3 caracteres.";
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return "E-mail e obrigatorio.";
        }

        if (!email.Contains('@'))
        {
            return "E-mail invalido.";
        }

        if (string.IsNullOrWhiteSpace(cpf))
        {
            return "CPF e obrigatorio.";
        }

        var cpfLimpo = cpf.Trim();

        if (cpfLimpo.Length != 11 || !long.TryParse(cpfLimpo, out _))
        {
            return "CPF invalido.";
        }

        return null;
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
