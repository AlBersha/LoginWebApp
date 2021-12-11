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

        public IActionResult LoginProcessing(LoginViewModel model)
        {
            return _domainService.LoginUser(new UserModel(model)) ? View("LoginSuccessfully") : View("LoginPage");
        }

        public IActionResult Register()
        {
            return View("~/Views/Register/Registration.cshtml");
        }
    }
}