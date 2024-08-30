using Microsoft.AspNetCore.Mvc;

namespace CST350_Minesweeper.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
