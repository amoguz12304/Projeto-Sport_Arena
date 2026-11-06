namespace ApiSportArena.Models
{
    public record UsuarioResponse(
        Guid Id,
        string Nome,
        int Idade,
        string Email,
        string Cpf,
        bool Status,
        string Funcao,
        string? Foto);
}
