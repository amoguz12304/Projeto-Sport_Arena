using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiSportArena.Models
{
    public class Carrinho
    {
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("IdUsuario")]
        public Guid UsuarioId { get; set; }

      
        public List<CarrinhoItem>? Itens { get; set; } = new List<CarrinhoItem>();
    }
}