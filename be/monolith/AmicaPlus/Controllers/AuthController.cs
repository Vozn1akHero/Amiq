using AmicaPlus.Base;
using AmicaPlus.Contracts;
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
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userAuthenticationDto)
        {
            return Ok();
        }
    }
}
