using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Domain.Interfaces;
using Domain.Models;

namespace Domain
{
    public class UserDomainService: IUserDomainService
    {
        private IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        
        public UserDomainService(IUserRepository repository)
        {
            _userRepository = repository;
            _cryptoService = new CryptoService();
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
            var salt = _cryptoService.GetSalt();
            user.Salt = BytesToString(salt);
            user.Password = BytesToString(_cryptoService.HashPassword(user.Password, salt));

            var (cnonce, card) = _cryptoService.EncryptData(user.Card.Data);
            user.Card.Nonce = BytesToString(cnonce);
            user.Card.Data = BytesToString(card);
            
            var (pnonce, phone) = _cryptoService.EncryptData(user.Phone.Data);
            user.Phone.Nonce = BytesToString(pnonce);
            user.Phone.Data = BytesToString(phone);
            
            return _userRepository.CreateUser(user);
        }

        public bool LoginUser(ref UserModel user)
        {
            var potentialUser = _userRepository.GetUserById(user.UserName);
            if (_cryptoService.IsRightPassword(user.Password, potentialUser.Salt, potentialUser.Password))
            {
                var data = Enumerable
                    .Range(0, potentialUser.Phone.Nonce.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(potentialUser.Phone.Nonce.Substring(x, 2), 16))
                    .ToArray();
                user.Phone = new DataStorageModel
                {
                    Data = Encoding.Default.GetString(_cryptoService.DecryptData(potentialUser.Phone.Data, data))
                };

                var card = Enumerable
                    .Range(0, potentialUser.Card.Nonce.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(potentialUser.Card.Nonce.Substring(x, 2), 16))
                    .ToArray();
                user.Card = new DataStorageModel
                {
                    Data = Encoding.Default.GetString(_cryptoService.DecryptData(potentialUser.Card.Data, card))
                };
                    
                return true;
            }

            return false;
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

        private string BytesToString(byte[] bytes)
        {
            return Regex.Replace(BitConverter.ToString(bytes).ToLower(), "-", "");
        }
    }
}