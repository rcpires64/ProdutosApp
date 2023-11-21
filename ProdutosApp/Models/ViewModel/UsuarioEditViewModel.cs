using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.Models.ViewModel
{
    public class UsuarioEditViewModel
    {
        [Required(ErrorMessage = "O id é obrigatório.")]
        public string Id { get; set; } = default!;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 255 caracteres.")]
        public string Nome { get; set; } = default!;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
        public string Email { get; set; } = default!;

    }
}
