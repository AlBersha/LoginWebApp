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
            return _userRepository.GetUserById(user.UserId);
        }

        public UserModel CreateUser(UserModel user)
        {
            user.Password = _cryptoService.HashPassword(user.Password);
            return _userRepository.CreateUser(user);
        }

        public bool LoginUser(UserModel user)
        {
            return _cryptoService.IsRightPassword(user.Password, _userRepository.GetUserById(user.UserId).Password);
        }

        public UserModel UpdateUserPassword(UpdatePasswordModel user)
        {
            var oldUser = _userRepository.GetUserById(user.UserId);
            if (_cryptoService.IsRightPassword(user.OldPassword, oldUser.Password))
            {
                user.NewPassword = _cryptoService.HashPassword(user.NewPassword);
            }

            return _userRepository.UpdateUserPassword(user);
        }

        public UserModel UpdateUserData(UserModel user)
        {
            return _userRepository.UpdateUser(user.UserId, user);
        }

        public UserModel DeleteUser(Guid userId)
        {
            return _userRepository.DeleteUser(userId);
        }
    }
}