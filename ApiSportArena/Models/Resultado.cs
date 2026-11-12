namespace ApiSportArena.Services
{
    public class Resultado<T>
    {
        public T? Valor { get; init; }
        public string? Erro { get; init; }
        public bool Sucesso => Erro is null;

        public static Resultado<T> Ok(T? valor) => new() { Valor = valor };
        public static Resultado<T> Falha(string erro) => new() { Erro = erro };
    }
}