using Amiq.Business.Group;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class GroupBlockedUserController : AmiqBaseController
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
