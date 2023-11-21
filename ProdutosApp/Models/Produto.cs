using ProdutosApp.Validations;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ProdutosApp.Models
{
    public class Produto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "A descrição do produto deve ter entre 3 e 255 caracteres.")]
        public string Descricao { get; set; } = default!;

        [Required(ErrorMessage = "O código EAN do produto é obrigatório.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "O código EAN tem que ter 13 caracteres.")]
        public string CodigoEAN { get; set; } = default!;

        [Required(ErrorMessage = "A URL da imagem do produto é obrigatória")]
        [Url(ErrorMessage = "Forneça uma URL válida")]
        [StringLength(255, MinimumLength = 7, ErrorMessage = "A URL só pode ter no máximo 255 caracteres.")]
        public string UrlImagem { get; set; } = string.Empty;


        [Range(0, 5, ErrorMessage = "A quantidade  deve ser entre 0 e 5.")]
        public int Quantidade { get; set; }


        [Required(ErrorMessage = "O preço do produto é obrigatório")]
        public double Preco { get; set; }

    }
}