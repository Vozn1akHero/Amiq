using Amiq.WebApi.Base;
using Amiq.Contracts;
using Amiq.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amiq.Business.Auth;
using Microsoft.AspNetCore.Http;
using Amiq.Core.Auth;

namespace Amiq.WebApi.Controllers
{
    public class AuthController : AmiqBaseController
    {
        private BsAuth _bsAuth = new BsAuth();

        [HttpPost("authenticate")]
        public ActionResult<DtoUserAuthenticationResult> Authenticate([FromBody] DtoUserAuthentication dtoUserAuthentication)
        {
            try
            {
                DtoUserAuthenticationResult result = _bsAuth.Authenticate(dtoUserAuthentication);
                if (result.Success)
                {
                    string jwt = JwtExtensions.GenerateJSONWebToken(result.JwtBase.UserId, result.JwtBase.UserEmail);
                    HttpContext.Response.Cookies.Append("token",jwt, new CookieOptions { HttpOnly = true });
                }
                return result.Success ? Ok(result) : new ForbidResult();
            } catch (Exception ex) {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] DtoUserRegistration dtoUserRegistration)
        {
            try
            {
                var userRegistartionResult = _bsAuth.Register(dtoUserRegistration);
                return Ok(userRegistartionResult);
            } catch {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
