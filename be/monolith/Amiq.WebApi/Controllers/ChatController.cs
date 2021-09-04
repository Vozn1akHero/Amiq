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

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class ChatController : AmiqBaseController
    {
        private BsChat _bsChat = new BsChat();
        private BsChatMessage _bsChatMessage = new BsChatMessage();

        [HttpGet("message")]
        public async Task<IActionResult> GetMessagesByUserId([FromQuery] int userId)
        {
            return await Task.FromResult(Ok());
        }

        [HttpPost("message")]
        public async Task<IActionResult> CreateMessage(DtoChatMessage dtoMessage)
        {
            return await Task.FromResult(Ok());
        }

        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessageById(Guid messageId)
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("previews")]
        [AuthorizeChatInterlocutor]
        public async Task<IActionResult> GetChatPreviewsAsync([FromQuery] DtoChatPreviewListRequest dtoChatPreviewListRequest)
        {
            var previews = await _bsChatMessage.GetChatPreviewListAsync(dtoChatPreviewListRequest);
            return previews != null ? Ok(previews) : NotFound();
        }

    }
}
