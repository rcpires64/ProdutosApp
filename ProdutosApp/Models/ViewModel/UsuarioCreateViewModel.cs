using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.Models.ViewModel
{
    public class UsuarioCreateViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 255 caracteres.")]
        public string Nome { get; set; } = default!;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [Display(Name = "Senha")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 16 caracteres.")]
        [DataType(DataType.Password)] // definição de typo pro form
        public string Senha { get; set; } = default!;

        [Required(ErrorMessage = "É necessário repetir a senha.")]
        [Display(Name = "Repita a Senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não são iguais.")]
        [DataType(DataType.Password)] // definição de typo pro form
        public string ValidSenha { get; set; } = default!;
    }
}
