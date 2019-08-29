using Biosis.BusinessLayer.Interface;
using Biosis.DataObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: Controller
    {
        private readonly IUserBusinessLayer _userBusinessLayer;

        public UserController(IUserBusinessLayer userBusinessLayer)
        {
            _userBusinessLayer = userBusinessLayer;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _userBusinessLayer.CreateUser(userDTO);

            return Ok();
        }
    }
}
