using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public IReadOnlyCollection<UserModel> GetAllUsers();
        public UserModel GetUserById (string username);
        public UserModel CreateUser (UserModel user);
        public UserModel UpdateUser (UserModel user);
        public UserModel UpdateUserPassword(UpdatePasswordModel user);
        public UserModel DeleteUser (string username);
    }
}