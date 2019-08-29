using Biosis.DataObject;
using Biosis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.BusinessLayer.Interface
{
    public interface IUserBusinessLayer
    {
        User CreateUser(UserDTO userDTO);
        User GetUser(Guid idUser);
    }
}
