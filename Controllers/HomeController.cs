using CST350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace CST350_Minesweeper.Controllers
{
    public class HomeController : Controller
    {
        private readonly SecurityDAO securitydao; 

        public HomeController(SecurityDAO injectedSecurityDAO)
        {
            securitydao = injectedSecurityDAO;
        }

        public IActionResult Index()
        {
            return View("Home");
        }

        // method that will strictly check if the email exists in the database
        [HttpPost]
        public IActionResult CheckEmail(string email)
        {
            bool emailExists = securitydao.isCurrentUser(email);

            if (emailExists)
            {
                return RedirectToAction("Index", "Login", new { email = email });
            }
            else
            {
                //If the emailDoesnt exist then error message can pop up in Home that says "email does not exist.. display error message
                ViewBag.ErrorMessage = "Email does not exist. Register New Email?";
                return View("Home");
            }
        }
    }
}
