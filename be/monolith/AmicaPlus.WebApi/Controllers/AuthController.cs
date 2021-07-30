using AmicaPlus.WebApi.Base;
using AmicaPlus.Contracts;
using AmicaPlus.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmicaPlus.Business.Auth;
using AmicaPlus.Modules.Mapping;
using AmicaPlus.ResultSets.Auth;
using Microsoft.AspNetCore.Http;

namespace AmicaPlus.WebApi.Controllers
{
    public class AuthController : AmicaPlusBaseController
    {
        private BsAuth _bsAuth = new BsAuth();


        [HttpPost("authenticate")]
        public ActionResult<RsUserAuthenticationResult> Authenticate([FromBody] DtoUserAuthentication dtoUserAuthentication)
        {
            try
            {
                RsUserAuthenticationResult result = new();
                var rsUserAuthentication = APAutoMapper.Instance.Map<RsUserAuthentication>(dtoUserAuthentication);
                result = _bsAuth.Authenticate(rsUserAuthentication);
                var rsUserAuthenticationResult = APAutoMapper.Instance.Map<DtoUserAuthenticationResult>(result);
                return result.Success ? Ok(rsUserAuthenticationResult) : new ForbidResult();
            } catch (Exception ex) {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] DtoUserRegistration dtoUserRegistration)
        {
            try
            {
                RsUserRegistartionResult rsUserRegistartionResult = new();
                var rsUserRegistration = APAutoMapper.Instance.Map<RsUserRegistration>(dtoUserRegistration);
                rsUserRegistartionResult = _bsAuth.Register(rsUserRegistration);
                var dto = APAutoMapper.Instance.Map<DtoUserRegistartionResult>(rsUserRegistartionResult);
                return Ok(dto);
            } catch {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
