using System.Text.RegularExpressions;
using FluentValidation;
using MentoriaDDD.Dtos;

namespace MentoriaDDD.Validador
{
    public class CriarClienteValidador : AbstractValidator<CriarClienteRequest>
    {
        public CriarClienteValidador()
        {
            ConfigurarRegras();
        }

        protected void ConfigurarRegras()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("Nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(100)
                .WithMessage("Nome deve ter no máximo 100 caracteres.")
                .Matches(@"^[a-záéíóúãõâêôç\s]+$", RegexOptions.IgnoreCase)
                .WithMessage("Nome não pode conter números ou caracteres especiais.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("E-mail inválido.");

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório.")
                .Must(cpf => ValidacaoCPF.Validar(cpf))
                .WithMessage("CPF inválido.");
        }
    }

    public class AtualizarClienteValidador : AbstractValidator<AtualizarClienteRequest>
    {
        public AtualizarClienteValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("Nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(100)
                .WithMessage("Nome deve ter no máximo 100 caracteres.")
                .Matches(@"^[a-záéíóúãõâêôç\s]+$", RegexOptions.IgnoreCase)
                .WithMessage("Nome não pode conter números ou caracteres especiais.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("E-mail inválido.");

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório.")
                .Must(cpf => ValidacaoCPF.Validar(cpf))
                .WithMessage("CPF inválido.");
        }
    }
}
