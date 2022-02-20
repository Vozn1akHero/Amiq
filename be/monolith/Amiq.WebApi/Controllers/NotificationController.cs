using Amiq.BusinessLayer.Notification;
using Amiq.Contracts.Notification;
using Amiq.Contracts.Utils;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class NotificationController : AmiqBaseController
    {
        private BlNotification _blNotification = new BlNotification();

        [HttpGet]
        public async Task<IActionResult> GetNotificationsAsync([FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var notifications = await _blNotification.GetNotificationsAsync(JwtStoredUserInfo.UserId, dtoPaginatedRequest);
            return Ok(notifications);
        }

        [HttpGet("any-not-read-exist")]
        [Produces(typeof(DtoNotReadNotificationsExistResult))]
        public async Task<IActionResult> AnyNotReadExistAsync()
        {
            var result = await _blNotification.AnyNotReadExistAsync(JwtStoredUserInfo.UserId);
            return Ok(result);
        }

        [HttpPut("set-all-read")]
        public async Task<IActionResult> SetAllReadAsync()
        {
            var result = await _blNotification.SetAllReadAsync(JwtStoredUserInfo.UserId);
            return Ok(result);
        }
    }
}
