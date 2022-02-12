using Amiq.Services.Common.Contracts;
using Amiq.Services.Notification.Base;
using Amiq.Services.Notification.BusinessLayer;
using Amiq.Services.Notification.Contracts.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Notification.Controllers
{
    public class NotificationController : AmiqBaseController
    {
        private BlNotification _blNotification = new BlNotification();

        [HttpGet]
        public async Task<IActionResult> GetNotificationsAsync([FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var notifications = await _blNotification.GetNotificationsAsync(JwtStoredUserId, dtoPaginatedRequest);
            return Ok(notifications);
        }

        [HttpGet("any-not-read-exist")]
        [Produces(typeof(DtoNotReadNotificationsExistResult))]
        public async Task<IActionResult> AnyNotReadExistAsync()
        {
            var result = await _blNotification.AnyNotReadExistAsync(JwtStoredUserId);
            return Ok(result);
        }

        [HttpPut("set-all-read")]
        public async Task<IActionResult> SetAllReadAsync()
        {
            var result = await _blNotification.SetAllReadAsync(JwtStoredUserId);
            return Ok(result);
        }
    }
}
