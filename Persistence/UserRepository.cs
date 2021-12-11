using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;

namespace Persistence
{
    public class UserRepository: IUserRepository
    {
        private ApplicationContext ApplicationContext { get; }

        public UserRepository(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }
        
        public IReadOnlyCollection<UserModel> GetAllUsers()
        {
            return ApplicationContext.UsersData.ToList();
        }

        public UserModel GetUserById(string username)
        {
            return ApplicationContext.UsersData.SingleOrDefault(user => user.UserName == username)!;
        }

        public UserModel CreateUser(UserModel user)
        {
            ApplicationContext.Add(user);

            try
            {
                ApplicationContext.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public UserModel UpdateUser(UserModel user)
        {
            var userToUpdate = ApplicationContext.UsersData.SingleOrDefault(model => model.UserName == user.UserName);

            if (userToUpdate == null)
            {
                throw new Exception();
            }

            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;

            try
            {
                ApplicationContext.SaveChanges();
                return userToUpdate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public UserModel UpdateUserPassword(UpdatePasswordModel user)
        {
            var userToUpdate = ApplicationContext.UsersData.SingleOrDefault(model => model.UserName == user.UserName);

            if (userToUpdate == null)
            {
                throw new Exception();
            }

            userToUpdate.Password = user.NewPassword;

            try
            {
                ApplicationContext.SaveChanges();
                return userToUpdate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public UserModel DeleteUser(string username)
        {
            var userToDelete = ApplicationContext.UsersData.SingleOrDefault(user => user.UserName == username);

            if (userToDelete != null)
            {
                throw new Exception();
            }

            ApplicationContext.UsersData.Remove(userToDelete);

            try
            {
                ApplicationContext.SaveChanges();
                return userToDelete;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}