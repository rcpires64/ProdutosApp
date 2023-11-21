using ProdutosApp.Models;
using ProdutosApp.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProdutosApp.Controllers.Web;

[Route("Login")]
[ApiExplorerSettings(IgnoreApi = true)]
public class LoginController: Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public LoginController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UsuarioLoginViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        var user = await _userManager.FindByEmailAsync(viewModel.Email);

        if (user != null)
        {
            // Verifica a senha e, se correta, faz o login do usuário
            var result = await _signInManager.PasswordSignInAsync(user, viewModel.Senha, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                return RedirectToAction("Index", "Usuarios"); // Redirecione para a página inicial ou painel
            }
        }

        ModelState.AddModelError(string.Empty, "Login inválido");

        return View(viewModel);
    }
}