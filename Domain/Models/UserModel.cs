using System;
using LoginApp.Models;

namespace Domain.Models
{
    public class UserModel
    {
        public string UserName;
        public string Email;
        public string Password;
        public string Salt;

        public UserModel()
        {
            
        }
        
        
        public UserModel(RegisterViewModel userData)
        {
            UserName = userData.UserName;
            Email = userData.Email;
            Password = userData.Password;
        }
    }
}