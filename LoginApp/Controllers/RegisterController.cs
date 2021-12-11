using Domain.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Controllers
{
    public class RegisterController : Controller
    {
        private IUserDomainService _domainService { get; set; }

        public RegisterController(IUserDomainService domainService)
        {
            _domainService = domainService;
        }
        
        public IActionResult Index()
        {
            return View("Registration");
        }

        public IActionResult RegisterUser(RegisterViewModel model)
        {
            var user = new UserModel(model);
            _domainService.CreateUser(user);
            return View("~/Views/Login/LoginSuccessfully.cshtml", user);
            
        }

        public IActionResult Login()
        {
            return View("~/Views/Login/LoginPage.cshtml");
        }
    }
}
