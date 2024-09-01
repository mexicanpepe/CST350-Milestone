using CST350_Minesweeper.Models;
using CST350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace CST350_Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        private readonly SecurityDAO _securitydao;

        public LoginController(SecurityDAO securitydao)
        {
            _securitydao = securitydao ?? throw new ArgumentNullException(nameof(securitydao));
        }

        public IActionResult Index(string email)
        {
            var user = new User { Email = email };
            return View("LoginForm", user);
        }

        [HttpPost]
        public IActionResult ProcessLogin(User user)
        {
            User currentUser = _securitydao.checkLogin(user);

            if (currentUser != null)
            {
                return View("PostLogin", currentUser);  // Redirect to the PostLogin view on successful login
            }
            else
            {
                return View("LoginFailure", user);  // Redirect to the LoginFailure view on unsuccessful login
            }
        }

        public IActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();

            // Redirect to login page
            return RedirectToAction("Index", "Login");
        }
    }
}
