using Amiq.Business.User;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class UserController : AmiqBaseController
    {
        private BsUser _bsUser = new BsUser();

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            //var user = await _bsUser.GetExtendedUserInfoAsync(JwtStoredUserInfo.UserId, userId);
            var user = await _bsUser.GetUserByIdAsync(1, userId);
            if(user == null) return NotFound();
            return Ok(user);
        }
    }
}
