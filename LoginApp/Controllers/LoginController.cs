using Domain.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Controllers
{
    public class LoginController : Controller
    {
        private IUserDomainService _domainService { get; set; }

        public LoginController(IUserDomainService domainService)
        {
            _domainService = domainService;
        }
        // GET
        public IActionResult Index()
        {
            return View("LoginPage");
        }

        public IActionResult LoginProcessing(LoginViewModel user)
        {
            var model = new UserModel(user);
            return _domainService.LoginUser(ref model) ? View("LoginSuccessfully", model) : View("LoginPage");
        }

        public IActionResult Register()
        {
            return View("~/Views/Register/Registration.cshtml");
        }
    }
}