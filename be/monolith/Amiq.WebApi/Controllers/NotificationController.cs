using Amiq.WebApi.Base;
using Amiq.Contracts.Chat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amiq.Business.Chat;
using Microsoft.AspNetCore.Authorization;
using Amiq.WebApi.Core.Auth;
using Amiq.WebApi.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Amiq.Contracts.Utils;
using System.Threading;
using Amiq.Business.Notification;

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
    }
}
