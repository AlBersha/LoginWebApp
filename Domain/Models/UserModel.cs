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
        public DataStorageModel Card { get; set; }
        public DataStorageModel Phone { get; set; }

        public UserModel()
        {
            UserName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;

            Card = new DataStorageModel();
            Phone = new DataStorageModel();
        }
        
        
        public UserModel(RegisterViewModel userData)
        {
            UserName = userData.UserName;
            Email = userData.Email;
            Password = userData.Password;
            Card = new DataStorageModel
            {
                Data = userData.CardNumber
            };
            Phone = new DataStorageModel
            {
                Data = userData.Phone
            };
        }

        public UserModel(LoginViewModel userData)
        {
            UserName = userData.UserName;
            Email = userData.Email;
            Password = userData.Password;
        }
    }
}