using Amiq.Services.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.Contracts.Utils;
using Amiq.Services.Group.Base;

namespace Amiq.Services.Group.Controllers
{
    public class GroupBlockedUserController : AmiqGroupBaseController
    {
        private BlBlockedGroupUser _blBlockedGroupUser = new BlBlockedGroupUser();

        [HttpGet("list/{groupId}")]
        public async Task<IActionResult> GetGroupBlockedUsersAsync(int groupId)
        {
            var blockedUsers = await _blBlockedGroupUser.GetGroupBlockedUsersAsync(groupId);
            return Ok(blockedUsers);
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockUserAsync([FromQuery] int userId, [FromQuery] int groupId)
        {
            await _blBlockedGroupUser.BlockUserAsync(userId, groupId);
            return Ok();
        }
    }
}
