using System.ComponentModel.DataAnnotations;
using Domain.Models.ViewModels;

namespace Domain.Models
{
    public class UserModel
    {
        [Required]
        public string UserName;
        [Required]
        public string Email;
        [Required]
        public string Password;
        public string Salt;

        public UserModel()
        {
            UserName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
        
        
        public UserModel(RegisterViewModel userData)
        {
            UserName = userData.UserName;
            Email = userData.Email;
            Password = userData.Password;
        }
        
        public UserModel(LoginViewModel userData)
        {
            UserName = userData.UserName;
            Email = userData.Email;
            Password = userData.Password;
        }
    }
}