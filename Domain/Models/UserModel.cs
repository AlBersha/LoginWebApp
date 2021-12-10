using System;
using LoginApp.Models;

namespace Domain.Models
{
    public class UserModel
    {
        public Guid UserId;
        public string UserName;
        public string Email;
        public string Password;

        public UserModel()
        {
            
        }
        
        
        public UserModel(RegisterViewModel userData)
        {
            UserId = Guid.NewGuid();
            UserName = userData.UserName;
            Email = userData.Email;
            Password = userData.Password;
        }
    }
}