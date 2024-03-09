using Microsoft.AspNetCore.Mvc;

namespace PARCIAL1B.Controllers
{
    public class Platos : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
