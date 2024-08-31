using CST350_Minesweeper.Models;
using CST350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CST350_Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        private readonly SecurityDAO securitydao;

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

        //if login successfull then display success page of fails then display failure page
        public IActionResult processLogin(Models.User user)
        {
            User currentUser = securitydao.checkLogin(user);

            if (currentUser != null) {
                //will redirect to Gameboard view
                //return RedirectToAction("Index", "Gameboard");
                return View("LoginSuccess", user);

            } else {
                return View("LoginFailure", user);
            }
        }


       
    }
}

