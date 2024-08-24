using CST350_Minesweeper.Models;
using CST350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CST350_Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            //dispay LoginForm.cshtml
            return View("LoginForm");
        }

        //if login successfull then display success page of fails then display failure page
        public IActionResult processLogin(Models.User user)
        {
            SecurityDAO securitydao = new SecurityDAO();
            User currentUser = securitydao.checkLogin(user);

            if (currentUser != null) {
                return View("LoginSuccess", user);

            } else {
                return View("LoginFailure", user);
            }
        }
    }
}

