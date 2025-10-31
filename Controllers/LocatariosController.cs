using Microsoft.EntityFrameworkCore;
using GestaoCondominio.Context;
using GestaoCondominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoCondominio.Controllers
{
    [Authorize]
    public class LocatariosController : Controller
    {
        private readonly AppDbContext _db;
        public LocatariosController(AppDbContext db) { _db = db; }

        [HttpGet]
        [Authorize(Roles = "Gestor,Sindico")]
        public IActionResult Create()
        {
            ViewBag.Imoveis = new SelectList(_db.Imoveis.ToList(), "Id", "Endereco");
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Gestor,Sindico,Morador")]
        public IActionResult Index()
        {
            var items = _db.Locatarios.Include(l => l.Imovel).ToList();
            return View(items);
        }

        [HttpGet]
        [Authorize(Roles = "Gestor,Sindico,Morador")]
        public IActionResult ListPartial()
        {
            var items = _db.Locatarios.Include(l => l.Imovel).ToList();
            return PartialView("_List", items);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Gestor,Sindico")]
    public async Task<IActionResult> Create(Locatario model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Imoveis = new SelectList(_db.Imoveis.ToList(), "Id", "Endereco");
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                    return Json(new { success = false, errors });
                }
                return View(model);
            }
            // garantir que campos que o banco espera não sejam nulos (evita SqlException quando coluna não permite NULL)
            model.Email = model.Email ?? string.Empty;
            model.CPF = model.CPF ?? string.Empty;
            model.Telefone = model.Telefone ?? string.Empty;

            _db.Locatarios.Add(model);
            await _db.SaveChangesAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }
    }
}
