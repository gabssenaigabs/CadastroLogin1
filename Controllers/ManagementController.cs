using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GestaoCondominio.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
