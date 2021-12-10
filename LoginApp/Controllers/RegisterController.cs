using LoginApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View("Registration");
        }

        public IActionResult RegisterUser(UserModel model)
        {
            return View("LoginSuccessfully");
        }
    }
}
