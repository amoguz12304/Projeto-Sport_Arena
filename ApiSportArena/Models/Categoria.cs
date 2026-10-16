using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiSportArena.Models
{
    public class Categoria
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria não pode estar em branco")]
        public string Nome { get; set; }

        // Navegação para produtos dessa categoria

        public ICollection<Produto>? Produtos { get; set; }

    }
}
