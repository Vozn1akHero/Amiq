using Amiq.WebApi.Base;
using Amiq.Contracts.Chat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Amiq.WebApi.Core.Auth;
using Amiq.WebApi.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Amiq.Contracts.Utils;
using System.Threading;
using Amiq.BusinessLayer.Chat;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class ChatController : AmiqBaseController
    {
        private BlChat _bsChat = new BlChat();
        private BlChatMessage _bsChatMessage = new BlChatMessage();

        [HttpGet("previews")]
        public async Task<IActionResult> GetChatPreviewsAsync([FromQuery] DtoPaginatedRequest dtoPaginatedRequest, 
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }
            var previews = await _bsChatMessage.GetChatPreviewListAsync(JwtStoredUserInfo.UserId, dtoPaginatedRequest);
            return Ok(previews);
        }

        [HttpGet("{chatParticipantId}")]
        public async Task<IActionResult> CanChatBeRun([FromRoute] int chatParticipantId)
        {
            return Ok();
        }

        [HttpGet("search")]
        [Produces(typeof(List<DtoChatPreview>))]
        public async Task<IActionResult> SearchAsync([FromQuery] string text, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(text);

            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }

            var previews = await _bsChatMessage.SearchForChatsAsync(JwtStoredUserInfo.UserId, text);
            return Ok(previews);
        }
    }
}
