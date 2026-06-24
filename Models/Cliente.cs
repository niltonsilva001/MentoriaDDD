namespace MentoriaDDD.Models;

public class Cliente
{
    public Cliente(string nome, string email, string cpf)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
        CriadoEm = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public string Cpf { get; private set; }

    public void AtualizarDados(string nome, string email, string cpf)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
    }
}
