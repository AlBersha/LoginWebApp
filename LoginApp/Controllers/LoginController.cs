using LoginApp.Models;
using LoginApp.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Controllers
{
    public class LoginController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View("LoginPage");
        }

        public IActionResult LoginProcessing(UserModel model)
        {
            var securityService = new SecurityService();

            if (securityService.IsValid(model))
            {
                return View("LoginSuccessfully", model);
            }

            return View("LoginPage", model);
        }

        // public IActionResult ToRegister()
        // {
        //     return View("Registration");
        // }
    }
}