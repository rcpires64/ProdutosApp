
using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.Models.ViewModel;

public class UsuarioLoginViewModel
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [Display(Name = "Senha")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = default!;
}