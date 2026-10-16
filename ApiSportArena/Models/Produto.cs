using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiSportArena.Models
{
    public class Produto    
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "A descrição do produto não pode exceder 500 caracteres")]
        public string Descricao { get; set; }

        [Precision(18, 2)]
        public decimal Preco { get; set; }

        
        [Required(ErrorMessage = "O identificador de categoria não pode estar em branco")]
        public Guid CategoriaId { get; set; }        
        
        [JsonIgnore]
        public Categoria? Categoria { get; set; }     

        public string Modelo { get; set; }
  
        public bool Disponivel { get; set; }

        public int Estoque { get; set; }

        public string? Imagem { get; set; }


    }

}

