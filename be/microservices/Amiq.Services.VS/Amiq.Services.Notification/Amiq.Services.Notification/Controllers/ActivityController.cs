using Amiq.Services.Notification.Base;
using Amiq.Services.Notification.Contracts.Activity;
using Amiq.Services.Notification.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Notification.Controllers
{
    public class ActivityController : AmiqBaseController
    {
        private DaoActivity _daActivity = new DaoActivity();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DtoCreateActivityInBulk dtoCreateActivityInBulk)
        {
            await _daActivity.CreateAsync(JwtStoredUserId, dtoCreateActivityInBulk);
            return Ok();
        }
    }
}
