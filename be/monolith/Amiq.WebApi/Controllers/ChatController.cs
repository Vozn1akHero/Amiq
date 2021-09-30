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

namespace Amiq.WebApi.Controllers
{
    //[Authorize]
    public class ChatController : AmiqBaseController
    {
        private BsChat _bsChat = new BsChat();
        private BsChatMessage _bsChatMessage = new BsChatMessage();


        [HttpGet("previews")]
        //[AuthorizeChatInterlocutor]
        public async Task<IActionResult> GetChatPreviewsAsync([FromQuery] int userId,
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var previews = await _bsChatMessage.GetChatPreviewListAsync(userId, dtoPaginatedRequest);
            return Ok(previews);
        }
    }
}
