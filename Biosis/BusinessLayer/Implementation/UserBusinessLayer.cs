using BCrypt;
using Biosis.BusinessLayer.Interface;
using Biosis.DataObject;
using Biosis.Model;
using Biosis.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.BusinessLayer.Implementation
{
    public class UserBusinessLayer: IUserBusinessLayer
    {

        private readonly IUserRepository _userRepository;

        public UserBusinessLayer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser(UserDTO userDTO)
        {
            var user = new User(userDTO);
            return _userRepository.Insert(user);
        }

        public User GetUser(Guid idUser)
        {
            return _userRepository.GetOne(idUser);
        }

        public bool Login(LoginDTO login, User user)
        {
            return BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
        }
    }
}
