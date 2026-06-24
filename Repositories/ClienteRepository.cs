using MentoriaDDD.Data;
using MentoriaDDD.Models;
using MentoriaDDD.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentoriaDDD.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente?> ObterPorCpfAsync(string cpf)
        {
            var cpfLimpo = cpf.Trim().Replace(".", "").Replace("-", "");

            return await _context.Clientes.FirstOrDefaultAsync(cliente =>
                cliente.Cpf.Trim().Replace(".", "").Replace("-", "") == cpfLimpo
            );
        }

        public async Task<Cliente?> ObterPorEmailAsync(string email)
        {
            var emailLimpo = email.Trim().ToLower();

            return await _context.Clientes.FirstOrDefaultAsync(cliente =>
                cliente.Email.Trim().ToLower() == emailLimpo
            );
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
