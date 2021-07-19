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
        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] DtoUserAuthentication dtoUserAuthentication)
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
