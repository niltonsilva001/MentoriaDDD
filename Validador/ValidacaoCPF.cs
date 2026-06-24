using System.Text.RegularExpressions;

namespace MentoriaDDD.Validador
{
    public static class ValidacaoCPF
    {
        public static bool Validar(string cpf)
        {
            var cpfLimpo = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpfLimpo.Length != 11 || !long.TryParse(cpfLimpo, out _))
                return false;

            // Rejeitar CPFs com todos os dígitos iguais
            if (cpfLimpo.All(c => c == cpfLimpo[0]))
                return false;

            // Validar 1º dígito verificador
            if (!ValidarDigito(cpfLimpo, 9, 10))
                return false;

            // Validar 2º dígito verificador
            if (!ValidarDigito(cpfLimpo, 10, 11))
                return false;

            return true;
        }

        private static bool ValidarDigito(string cpf, int posicaoDigito, int multiplicadorInicial)
        {
            int soma = 0;
            for (int i = 0; i < posicaoDigito; i++)
                soma += int.Parse(cpf[i].ToString()) * (multiplicadorInicial - i);

            int resto = soma % 11;
            int digito = resto < 2 ? 0 : 11 - resto;

            return digito == int.Parse(cpf[posicaoDigito].ToString());
        }
    }
}
