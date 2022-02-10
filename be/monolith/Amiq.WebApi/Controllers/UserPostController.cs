using Amiq.BusinessLayer.Post;
using Amiq.BusinessLayer.Utils;
using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    [Route("api/user-post")]
    public class UserPostController : AmiqBaseController
    {
        private BlUserPost bsUserPost = new BlUserPost();

        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetPostsByUserId([FromRoute] int userId, [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var data = await bsUserPost.GetUserPostsAsync(userId, dtoPaginatedRequest);
            return Ok(data);
        }

        [HttpPatch]
        public async Task<IActionResult> EditAsync([FromBody] DtoEditUserPostRequest dtoEditUserPostRequest)
        {
            await bsUserPost.EditAsync(dtoEditUserPostRequest);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DtoPostCreation dtoPost)
        {
            try
            {
                var data = await bsUserPost.CreateAsync(JwtStoredUserInfo.UserId, dtoPost);
                return CreatedAtAction(nameof(CreateAsync), data);
            } catch (BsRuleIsBrokenException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
