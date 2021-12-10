using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserDomainService
    {
        public IReadOnlyCollection<UserModel> GetAllUsers();
        public UserModel GetUserById (UserModel user);
        public UserModel CreateUser (UserModel user);
        public bool LoginUser(UserModel user);
        public UserModel UpdateUserData (UserModel user);
        public UserModel UpdateUserPassword(UpdatePasswordModel user);
        public UserModel DeleteUser (Guid userId);
    }
}