using Amiq.BusinessLayer.Group;
using Amiq.Contracts.Group;
using Amiq.WebApi.Base;
using Amiq.WebApi.Core.Auth;
using Amiq.WebApi.Core.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class GroupController : AmiqBaseController
    {
        private BlGroup blGroup = new BlGroup();
        private ILocalFileStorage _localFileStorage;

        public GroupController(ILocalFileStorage localFileStorage)
        {
            _localFileStorage = localFileStorage;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync([FromBody] DtoCreateGroup dtoCreateGroup)
        {
            DtoGroupCard group = await blGroup.CreateGroupAsync(JwtStoredUserInfo.UserId, dtoCreateGroup);
            return CreatedAtAction(nameof(CreateGroupAsync), group);
        }

        [HttpPost("drop")]
        //[AuthorizeMainGroupAdmin]
        public async Task<IActionResult> DropGroup([FromBody] DtoDropGroupRequest dtoDropGroupRequest)
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var data = await blGroup.GetByName(JwtStoredUserInfo.UserId, name);
            return Ok(data);
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupById(int groupId)
        {
            var data = await blGroup.GetGroupById(groupId);
            if(data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut("edit")]
        public IActionResult Edit([FromBody] DtoEditGroupData dtoEditGroupDataRequest)
        {
            var result = blGroup.Edit(dtoEditGroupDataRequest);
            return Ok(result);
        }

        [HttpGet("user-params/{groupId}")]
        public async Task<IActionResult> GetGroupUserParamsAsync([FromRoute] int groupId)
        {
            var result = await blGroup.GetGroupUserParamsAsync(JwtStoredUserInfo.UserId, groupId);
            return Ok(result);
        }

        [HttpPost("change-avatar/{groupId}")]
        public async Task<IActionResult> ChangeGroupAvatar([FromRoute] int groupId, [FromForm(Name = "file")] IFormFile file)
        {
            var uploadResult = await _localFileStorage.UploadFileAsync(file);
            var result = blGroup.ChangeGroupAvatar(groupId, uploadResult.GeneratedFileName);
            return Ok(result);
        }
    }
}
