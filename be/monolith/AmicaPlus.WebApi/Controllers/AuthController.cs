using AmicaPlus.WebApi.Base;
using AmicaPlus.Contracts;
using AmicaPlus.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmicaPlus.Business.Auth;

namespace AmicaPlus.WebApi.Controllers
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
            var res = new BsAuth().GetEftests();
            return Ok(res);
        }

    }
}
