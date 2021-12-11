using System.ComponentModel.DataAnnotations;

namespace Domain.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        public LoginViewModel()
        {
            UserName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
        
        public LoginViewModel(UserModel user)
        {
            UserName = user.UserName;
            Email = user.Email;
        }
    }
}