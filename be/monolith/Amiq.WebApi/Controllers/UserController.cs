using Amiq.Business.User;
using Amiq.WebApi.Base;
using Amiq.WebApi.Core.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class UserController : AmiqBaseController
    {
        private BlUser _blUser = new BlUser();
        private ILocalFileStorage _localFileStorage;

        public UserController(ILocalFileStorage localFileStorage)
        {
            _localFileStorage = localFileStorage;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var user = await _blUser.GetUserByIdAsync(JwtStoredUserInfo.UserId, userId);
            //var user = await _bsUser.GetUserByIdAsync(1, userId);
            if(user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("change-avatar")]
        public async Task<IActionResult> ChangeAvatar([FromForm(Name = "file")] IFormFile file)
        {
            var uploadResult = await _localFileStorage.UploadFileAsync(file);
            var result = _blUser.ChangeAvatar(JwtStoredUserInfo.UserId, uploadResult.GeneratedFileName);
            return Ok(result);
        }
    }
}
