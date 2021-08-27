using AmicaPlus.WebApi.Base;
using AmicaPlus.Contracts;
using AmicaPlus.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmicaPlus.Business.Auth;
using Microsoft.AspNetCore.Http;
using AmicaPlus.Mapping;
using AmicaPlus.Core.Auth;

namespace AmicaPlus.WebApi.Controllers
{
    public class AuthController : AmicaPlusBaseController
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
                    HttpContext.Response.Cookies.Append("token",
                        jwt, 
                        new CookieOptions { HttpOnly = true });
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
                DtoUserRegistartionResult rsUserRegistartionResult = new();
                var rsUserRegistration = APAutoMapper.Instance.Map<DtoUserRegistration>(dtoUserRegistration);
                rsUserRegistartionResult = _bsAuth.Register(rsUserRegistration);
                var dto = APAutoMapper.Instance.Map<DtoUserRegistartionResult>(rsUserRegistartionResult);
                return Ok(dto);
            } catch {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
