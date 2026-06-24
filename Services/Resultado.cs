namespace MentoriaDDD.Services;

public class Resultado<T>
{
    private Resultado(bool sucesso, T? valor, string? erro)
    {
        Sucesso = sucesso;
        Valor = valor;
        Erro = erro;
    }

    public bool Sucesso { get; }
    public T? Valor { get; }
    public string? Erro { get; }

    public static Resultado<T> Ok(T valor)
    {
        return new Resultado<T>(true, valor, null);
    }

    public static Resultado<T> Falha(string erro)
    {
        return new Resultado<T>(false, default, erro);
    }
}
