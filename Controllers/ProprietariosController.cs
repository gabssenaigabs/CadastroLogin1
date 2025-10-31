using GestaoCondominio.Context;
using GestaoCondominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace GestaoCondominio.Controllers
{
    [Authorize]
    public class ProprietariosController : Controller
    {
        private readonly AppDbContext _db;
        public ProprietariosController(AppDbContext db) { _db = db; }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpGet]
        [Authorize(Roles = "Gestor,Sindico,Morador")]
        public IActionResult Index()
        {
            var items = _db.Proprietarios.ToList();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proprietario model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Proprietarios.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
