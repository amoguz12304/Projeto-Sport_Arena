namespace ApiSportArena.Models
{
    public static class Funcoes
    {
        public const string Admin = "Admin";
        public const string Cliente = "Cliente";
        public const string Gerente = "Gerente";
        public const string Funcionario = "Funcionário";

        // Funções que a API pode atribuir. "Admin" fica de fora de propósito.
        public static readonly string[] Atribuiveis = [Cliente, Gerente, Funcionario];
    }
}