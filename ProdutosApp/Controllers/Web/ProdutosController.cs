using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosApp.Data;
using ProdutosApp.Models;
using ProdutosApp.Validations;
using ProdutosApp.Extensions;
using ProdutosApp.Pages;
using ProdutosApp.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Drawing;
using System.Globalization;

namespace ProdutosApp.Controllers.Web
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProdutosController : Controller
    {
        private readonly ProdutosAppContext _context;
        private readonly ILogger<ProdutosController> _logger;
        private readonly UserManager<Usuario> _userManager;

        public ProdutosController(ProdutosAppContext context, ILogger<ProdutosController> logger, UserManager<Usuario> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: Produtos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Se o usuário não estiver autenticado, retorne uma mensagem específica
                return View("UsuarioNaoAutenticado");
            }
            return _context.Produtos != null ?
                        View(await _context.Produtos.ToListAsync()) :
                        Problem("Entity set 'ProdutosAppContext.Produto'  is null.");
        }


        // GET: Produtos
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            return _context.Produtos != null ?
                        View(await _context.Produtos.ToListAsync()) :
                        Problem("Entity set 'ProdutosAppContext.Produto'  is null.");
        }

        // GET: Produtos/Details/5
        [HttpGet("Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,CodigoEAN,UrlImagem,Quantidade,Preco")] Produto produto)
        {
            if (!ModelState.IsValid) return View(produto);

            ModelState.AddModelErrorIfNotEmpty("Descrição", produto.Descricao.ValidarDescricao());
            ModelState.AddModelErrorIfNotEmpty("Código EAN", produto.CodigoEAN.ValidarCodigoEAN());
            ModelState.AddModelErrorIfNotEmpty("URL Imagem", produto.UrlImagem.ValidarUrlImagem());
            ModelState.AddModelErrorIfNotEmpty("Quantidade", produto.Quantidade.ValidarQuantidade());
            ModelState.AddModelErrorIfNotEmpty("Preço", produto.Preco.ValidarPreco());

            if (ModelState.IsValid)
            {
                produto.Id = Guid.NewGuid();
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        // GET: Produtos/Edit/5
        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // PUT: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [Route("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Descricao,CodigoEAN,UrlImagem,Quantidade,Preco")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }
            produto.Preco = produto.Preco /100;

            if (!ModelState.IsValid) return View(produto);

            ModelState.AddModelErrorIfNotEmpty("Descrição", produto.Descricao.ValidarDescricao());
            ModelState.AddModelErrorIfNotEmpty("Código EAN", produto.CodigoEAN.ValidarCodigoEAN());
            ModelState.AddModelErrorIfNotEmpty("URL Imagem", produto.UrlImagem.ValidarUrlImagem());
            ModelState.AddModelErrorIfNotEmpty("Quantidade", produto.Quantidade.ValidarQuantidade());
            ModelState.AddModelErrorIfNotEmpty("Preço", produto.Preco.ValidarPreco());

            //produto.Preco = produto.Preco.ToString("N2", CultureInfo.InvariantCulture);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.CodigoEAN))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { success = true, redirectToUrl = Url.Action("Index", "Produtos") });
            }

            return View(produto);
        }

        // GET: Produtos/Delete/5
        [HttpGet("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // DELETE: Produtos/Delete/5
        [HttpDelete("Delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'ProdutosAppContext.Produto'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            return Json(new { success = true, redirectToUrl = Url.Action("Index", "Produtos") });
        }

        private bool ProdutoExists(string EAN)
        {
          return (_context.Produtos?.Any(e => e.CodigoEAN == EAN)).GetValueOrDefault();
        }
    }
}
