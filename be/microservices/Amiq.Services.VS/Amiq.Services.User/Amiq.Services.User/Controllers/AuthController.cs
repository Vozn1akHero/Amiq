using Amiq.Services.User.Base;
using Amiq.Services.User.BusinessLayer;
using Amiq.Services.User.Contracts.Auth;
using Amiq.Services.User.Core.Auth;
using Amiq.Services.User.Messaging;
using Amiq.Services.User.Messaging.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.User.Controllers
{
    public class AuthController : AmiqBaseController
    {
        private BlAuth _bsAuth = new BlAuth();

        [HttpPut("change-password")]
        //[Authorize]
        public IActionResult ChangePassword([FromBody] DtoChangeUserPassword dtoChangeUserPassword)
        {
            var result = _bsAuth.ChangePassword(JwtStoredUserId.Value, dtoChangeUserPassword);
            return Ok(result);
        }

        [HttpPut("change-email")]
        //[Authorize]
        public IActionResult ChangeEmail([FromBody] string email)
        {
            var result = _bsAuth.ChangeEmail(JwtStoredUserId.Value, email);
            return Ok(result);
        }

        [HttpPost("authenticate")]
        public ActionResult<DtoUserAuthenticationResult> Authenticate([FromBody] DtoUserAuthentication dtoUserAuthentication)
        {
            try
            {
                DtoUserAuthenticationResult result = _bsAuth.Authenticate(dtoUserAuthentication);
                if (result.Success)
                {
                    var jwt = JwtExtensions.GenerateJSONWebToken(result.JwtBase);
                    Response.Cookies.Append("token", jwt.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        //IsEssential = true,
                        //SameSite = SameSiteMode.None,
                        //Secure = true,
                    });
                }
                return result.Success ? Ok(result) : new ForbidResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] DtoUserRegistration dtoUserRegistration)
        {
            try
            {
                //dtoUserRegistration.Sex = EnumExtensions.TryMapStrValueToAltValue(typeof(EnSex), dtoUserRegistration.Sex);
                //var userRegistartionResult = _bsAuth.Register(dtoUserRegistration);

                //UserAmqpSender.Send(EnUserAmqpEvent.USER_CREATED.ToString(), userRegistartionResult.BasicUserInfo);
                var @event = new UserModificationEvent(
                    1,
                    "TEST5",
                    "Test2",
                    "user.jpg"
                );
                @event.EventName = "UserModificationEvent";
                RabbitMQPublisher.Publish(@event);

                //return Ok(userRegistartionResult);
                return Ok();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("logout")]
        //[Authorize]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            return Ok();
        }

        [HttpGet("validate-credentials")]
        //[Authorize]
        public IActionResult ValidateCredentials()
        {
            return Ok(JwtStoredUserId);
        }
    }
}
