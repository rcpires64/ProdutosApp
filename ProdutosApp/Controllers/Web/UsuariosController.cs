using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Models;
using ProdutosApp.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ProdutosApp.Controllers.Web;

[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class UsuariosController : Controller
{
    private readonly UserManager<Usuario> _userManager;

    public UsuariosController(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    // GET: Usuarios
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            // Se o usuário não estiver autenticado, retorne uma mensagem específica
            return View("UsuarioNaoAutenticado");
        }
        var usuarios = await _userManager.Users.ToListAsync();
        return View(usuarios);
    }

    // GET: Usuarios/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(string? id)
    {

        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // GET: Usuarios/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Usuarios/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Email,Senha,ValidSenha")] UsuarioCreateViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var usuario = new Usuario
        {
            UserName = viewModel.Nome,
            Email = viewModel.Email
        };

        var result = await _userManager.CreateAsync(usuario, viewModel.Senha);

        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(viewModel);
    }

    // GET: Usuarios/Edit/5
    [HttpGet("Edit/{id:Guid}")]
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var editViewModel = new UsuarioEditViewModel
        {
            Id = user.Id,
            Nome = user.UserName!,
            Email = user.Email!
        };

        return View(editViewModel);
    }

    // PUT: Usuarios/Edit/5
    [HttpPut]
    [Route("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] string id, [Bind("Id,Nome,Email")] UsuarioEditViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(viewModel);


        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        user.UserName = viewModel.Nome;
        user.Email = viewModel.Email;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Json(new { success = true, redirectToUrl = Url.Action("Index", "Usuarios") });

        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(viewModel);



    }

    // GET: Usuarios/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // POST: Usuarios/Delete/5
    [HttpDelete("Delete/{id}")]
    //[Route("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Json(new { success = true, redirectToUrl = Url.Action("Index", "Usuarios") });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View("Delete", user);

    }

}