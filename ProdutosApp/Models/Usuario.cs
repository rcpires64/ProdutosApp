using ProdutosApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.Models;

[Index(nameof(Email), IsUnique = true)]
public class Usuario : IdentityUser
{
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [Display(Name = "Tipo")]
    public UsuarioTipo Type { get; set; } = UsuarioTipo.Usuario;
    
    [Display(Name = "Data de Criação")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}