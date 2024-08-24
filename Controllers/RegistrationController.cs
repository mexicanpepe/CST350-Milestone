using CST350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace CST350_Minesweeper.Controllers
{
    public class RegistrationController : Controller
    {
        //view will display RegistrationFrom.cshtml
        public IActionResult Index()
        {
            return View("RegistrationForm");
        }

        //if user exists then display Failure and user doesnt exist already then display success page
        [HttpPost]
        public IActionResult processRegistration(Models.User user)
        {
            SecurityDAO securitydao = new SecurityDAO();

            if (securitydao.isCurrentUser(user.Email))
            {
                //Will display RegistrationFailure.cshtml
                return View("RegistrationFailure", user);
            } else
            {
                //Will add user to the DB and then display RegistrationSuccess.cshtml
                securitydao.AddUser(user);
                return View("RegistrationSuccess", user);
            }
        }
    }
}
