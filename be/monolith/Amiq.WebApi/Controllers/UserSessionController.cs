using Amiq.BusinessLayer.User;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class UserSessionController : AmiqBaseController
    {
        private BlUserSession _blUserOnlineStatus = new BlUserSession();

        [HttpPost("signal")]
        [Authorize]
        public IActionResult SignalAsync()
        {
            _blUserOnlineStatus.SignalAsync(JwtStoredUserInfo.UserId);
            return Ok();
        }
    }
}
