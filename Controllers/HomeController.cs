using CST350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace CST350_Minesweeper.Controllers
{
    public class HomeController : Controller
    {
        private readonly SecurityDAO _securitydao;  // Class-level field for SecurityDAO

        public HomeController(SecurityDAO securitydao)
        {
            _securitydao = securitydao ?? throw new ArgumentNullException(nameof(securitydao));  // Assign parameter to the field
        }

        public IActionResult Index()
        {
            return View("Home");
        }

        // Method that will strictly check if the email exists in the database
        [HttpPost]
        public IActionResult CheckEmail(string email)
        {
            bool emailExists = _securitydao.isCurrentUser(email);  // Use the correctly assigned field

            if (emailExists)
            {
                return RedirectToAction("Index", "Login", new { email = email });
            }
            else
            {
                // If the email doesn't exist, display an error message
                ViewBag.ErrorMessage = "Email does not exist. Register New Email?";
                return View("Home");
            }
        }
    }
}
