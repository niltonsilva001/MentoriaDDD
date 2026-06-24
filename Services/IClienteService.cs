using MentoriaDDD.Dtos;

namespace MentoriaDDD.Services;

public interface IClienteService
{
    Task<IReadOnlyCollection<ClienteResponse>> ObterTodosAsync();
    Task<ClienteResponse?> ObterPorIdAsync(int id);
    Task<ClienteResponse?> ObterPorCPFAsync(string cpf);
    Task<Resultado<ClienteResponse>> CriarAsync(CriarClienteRequest request);
    Task<Resultado<ClienteResponse>> AtualizarAsync(int id, AtualizarClienteRequest request);
    Task<bool> RemoverAsync(int id);
}
