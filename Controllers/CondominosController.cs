using GestaoCondominio.Context;
using GestaoCondominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestaoCondominio.Controllers
{
    [Authorize]
    public class CondominosController : Controller
    {
        private readonly AppDbContext _db;
        public CondominosController(AppDbContext db) { _db = db; }

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
            var items = _db.Condominos.Include(c => c.Imovel).ToList();
            return View(items);
        }

        [HttpGet]
        [Authorize(Roles = "Gestor,Sindico,Morador")]
        public IActionResult ListPartial()
        {
            var items = _db.Condominos.Include(c => c.Imovel).ToList();
            return PartialView("_List", items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Sindico")]
        public async Task<IActionResult> Create(Condomino model)
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
            _db.Condominos.Add(model);
            await _db.SaveChangesAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }
    }
}
