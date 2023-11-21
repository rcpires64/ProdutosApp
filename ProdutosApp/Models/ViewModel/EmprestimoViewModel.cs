using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.Models.ViewModel
{
    public class EmprestimoViewModel
    {
        [Required(ErrorMessage = "O produto é obrigatório.")]
        [Display(Name = "Produto")]
        public Guid ProdutoId { get; set; }

        [Display(Name = "Descricao do Produto")]
        public string Descricao { get; set; } = default!;

        [Required(ErrorMessage = "O usuário deve ser informado.")]
        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; } = default!;

        [Required]
        [Display(Name = "Produto Disponível")]
        public bool ProdutoDisponivel { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Retirada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly DataRetirada { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Prevista de Devolução")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly DataPrevistaDevolucao { get; set; }
    }
}
