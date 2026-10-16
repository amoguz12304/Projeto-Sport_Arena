using Microsoft.EntityFrameworkCore;
using System.ClientModel.Primitives;
using System.ComponentModel.DataAnnotations;
namespace ApiSportArena.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome não pode estar em branco")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A idade não pode estar em branco")]
        [Range(1, 100)]
        public int Idade {  get; set; }

        [Required(ErrorMessage = "O E-mail não pode estar em branco")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF não pode estar em branco")]
        [StringLength(11, MinimumLength = 11, ErrorMessage ="O CPF deve conter exatamente 11 números")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A senha não pode estar em branco")]
        [StringLength (100, MinimumLength = 8, ErrorMessage = "A senha deve conter ao mínimo 8 caracteres")]
        public string Senha { get; set; }

        public bool Status { get; set; } = true;

        [Required]
         public string Funcao { get; set; } = "Cliente";

        public string? Foto  { get; set; }

  

    }
}
