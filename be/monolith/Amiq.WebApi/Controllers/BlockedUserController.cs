using Amiq.Business.User;
using Amiq.Contracts.User;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class BlockedUserController : AmiqBaseController
    {
        private BsBlockedUser _bsBlockedUser = new BsBlockedUser();

        [HttpGet("is-blocked/{userId}")]
        public IActionResult IsUserBlockedByAnotherUser(int userId)
        {
            bool result = _bsBlockedUser.IsUserBlockedByAnotherUser(JwtStoredUserInfo.UserId, userId);
            return Ok(result);
        }

        [HttpPost("block/{userId}")]
        public IActionResult BlockUser(int userId)
        {
            var dto = new DtoUserBlockRequest
            {
                IssuerId = JwtStoredUserInfo.UserId,
                DestUserId = userId
            };
            _bsBlockedUser.BlockUser(dto);
            return Ok();
        }

        [HttpPost("unblock/{userId}")]
        public async Task<IActionResult> UnblockUser(int userId)
        {
            var dto = new DtoUserBlockRequest
            {
                IssuerId = JwtStoredUserInfo.UserId,
                DestUserId = userId
            };
            await _bsBlockedUser.UnblockUser(dto);
            return Ok();
        }
    }
}
