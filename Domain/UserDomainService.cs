using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Models;

namespace Domain
{
    public class UserDomainService: IUserDomainService
    {
        private IUserRepository _userRepository;
        private readonly CryptoService _cryptoService = new();

        public UserDomainService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public IReadOnlyCollection<UserModel> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public UserModel GetUserById(UserModel user)
        {
            return _userRepository.GetUserById(user.UserName);
        }

        public UserModel CreateUser(UserModel user)
        {
            user.Password = _cryptoService.GetHashString(_cryptoService.HashPassword(user.Password, _cryptoService.GetSalt()));
            return _userRepository.CreateUser(user);
        }

        public bool LoginUser(UserModel user)
        {
            // return _cryptoService.IsRightPassword(user.Password, _userRepository.GetUserById(user.UserName).Password);
            return true;
        }

        public UserModel UpdateUserPassword(UpdatePasswordModel user)
        {
            var oldUser = _userRepository.GetUserById(user.UserName);
            // if (_cryptoService.IsRightPassword(user.OldPassword, oldUser.Password))
            // {
            //     user.NewPassword = _cryptoService.HashPassword(user.NewPassword);
            // }

            return _userRepository.UpdateUserPassword(user);
        }

        public UserModel UpdateUserData(UserModel user)
        {
            return _userRepository.UpdateUser(user);
        }

        public UserModel DeleteUser(string username)
        {
            return _userRepository.DeleteUser(username);
        }
    }
}