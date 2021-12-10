using Domain.Interfaces;
using Domain.Models;
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

        public IActionResult LoginProcessing(UserModel model)
        {
            return _domainService.LoginUser(model) ? View("LoginSuccessfully") : View("LoginPage", model);
        }

        public IActionResult Register()
        {
            return View("~/Views/Register/Registration.cshtml");
        }
    }
}