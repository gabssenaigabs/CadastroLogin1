using GestaoCondominio.Context;
using GestaoCondominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace GestaoCondominio.Controllers
{
    [Authorize]
    public class ImoveisController : Controller
    {
        private readonly AppDbContext _db;
        public ImoveisController(AppDbContext db) { _db = db; }

        [HttpGet]
        [Authorize(Roles = "Gestor,Sindico,Morador")]
        public IActionResult Index()
        {
            var items = _db.Imoveis.ToList();
            return View(items);
        }

        [HttpGet]
        [Authorize(Roles = "Gestor")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create(Imovel model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Imoveis.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
