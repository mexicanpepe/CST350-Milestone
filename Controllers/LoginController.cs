using CST350_Minesweeper.Models;
using CST350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CST350_Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        private readonly SecurityDAO _securitydao;

        public LoginController(SecurityDAO injectedSecurityDAO)
        {
            securitydao = injectedSecurityDAO;
        }
        

        public IActionResult Index(string email)
        {
            var user = new User { Email = email };
            //dispay LoginForm.cshtml with email from Home input
            return View("LoginForm", user);
        }

        [HttpPost]
        public IActionResult ProcessLogin(User user)
        {
            User currentUser = _securitydao.checkLogin(user);

            if (currentUser != null)
            {
                return View("PostLogin", currentUser);  
            }
            else
            {
                return View("LoginFailure", user); 
            }
        }

    }
}
