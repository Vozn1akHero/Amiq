using AmicaPlus.Base;
using AmicaPlus.Contracts;
using AmicaPlus.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Controllers
{
    public class AuthController : AmicaPlusBaseController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] DtoUserAuthentication dtoUserAuthentication)
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DtoUserRegistration dtoUserRegistration)
        {
            return Ok();
        }

    }
}
