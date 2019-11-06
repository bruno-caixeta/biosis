using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biosis.DataObject;
using Microsoft.AspNetCore.Mvc;

namespace Biosis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            return Ok(loginDTO);
        }
    }
}