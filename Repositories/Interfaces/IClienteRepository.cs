using MentoriaDDD.Models;

namespace MentoriaDDD.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObterTodosAsync();
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<Cliente?> ObterPorCpfAsync(string cpf);
        Task<Cliente?> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task RemoverAsync(Cliente cliente);
    }
}
