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
using Amiq.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
                    var jwt = JwtExtensions.GenerateJSONWebToken(result.JwtBase);
                    Response.Cookies.Append("token", jwt.Token, new CookieOptions { 
                        HttpOnly = true,
                        //IsEssential = true,
                        //SameSite = SameSiteMode.None,
                        //Secure = true,
                    });
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
                dtoUserRegistration.Sex = EnumExtensions.TryMapStrValueToAltValue(typeof(EnSex), dtoUserRegistration.Sex);
                var userRegistartionResult = _bsAuth.Register(dtoUserRegistration);
                return Ok(userRegistartionResult);
            } catch {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("validate-credentials")]
        [Authorize]
        public IActionResult ValidateCredentials()
        {
            return Ok();
        }
    }
}
