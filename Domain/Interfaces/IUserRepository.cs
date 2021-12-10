using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public IReadOnlyCollection<UserModel> GetAllUsers();
        public UserModel GetUserById (Guid userId);
        public UserModel CreateUser (UserModel user);
        public UserModel UpdateUser (Guid id, UserModel user);
        public UserModel UpdateUserPassword(UpdatePasswordModel user);
        public UserModel DeleteUser (Guid userId);
    }
}