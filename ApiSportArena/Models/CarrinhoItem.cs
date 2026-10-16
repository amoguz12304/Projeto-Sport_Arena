using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiSportArena.Models
{
    public class CarrinhoItem
    {
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("CarrinhoId")]
        public Guid CarrinhoId { get; set; }

        [Required]
        [ForeignKey("ProdutoId")]
        public Guid ProdutoId { get; set; }

        public int Quantidade { get; set; }

        [JsonIgnore]
        public Carrinho? Carrinho { get; set; }

        public Produto? Produto { get; set; }

    }
}

