using BCrypt;
using Biosis.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Research> Researches { get; set; }

        public User()
        {

        }

        public User(UserDTO userDTO)
        {
            Name = userDTO.Name;
            Login = userDTO.Login;
            Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
    }
}
