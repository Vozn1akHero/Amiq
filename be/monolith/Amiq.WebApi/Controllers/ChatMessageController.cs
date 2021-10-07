using Amiq.Business.Chat;
using Amiq.Contracts.Chat;
using Amiq.Contracts.Utils;
using Amiq.WebApi.Base;
using Amiq.WebApi.Core.Auth;
using Amiq.WebApi.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    /// <summary>
    /// Zarządzanie wiadomościami użytkowników
    /// </summary>
    [Authorize]
    public class ChatMessageController : AmiqBaseController
    {
        private BsChatMessage _bsChatMessage = new BsChatMessage();
        private ISignalRChatService _signalRChatService;

        public ChatMessageController(ISignalRChatService signalRChatService)
        {
            _signalRChatService = signalRChatService;
        }

        [HttpGet("list-by-chat")]
        [AuthorizeChatInterlocutor]
        public async Task<IActionResult> GetChatMessagesByChatId([FromQuery] Guid chatId, [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var res = await _bsChatMessage.GetChatMessagesAsync(JwtStoredUserInfo.UserId, chatId, dtoPaginatedRequest);
            return Ok(res);
        }

        [HttpDelete]
        [AuthorizeChatInterlocutor]
        public async Task<IActionResult> DeleteMessageById([FromQuery] DtoDeleteChatMessageRequest dtoDeleteChatMessageRequest)
        {
            dtoDeleteChatMessageRequest.IssuerId = JwtStoredUserInfo.UserId;
            var result = await _bsChatMessage.DeleteMessageAsync(dtoDeleteChatMessageRequest);
            return Ok(result);
        }

        [HttpPost]
        [AuthorizeChatInterlocutor]
        public async Task<IActionResult> CreateMessage([FromQuery] Guid chatId, [FromBody] DtoChatMessageCreation dtoMessage)
        {
            var createdMsg = await _bsChatMessage.CreateChatMessageAsync(dtoMessage);
            await _signalRChatService.PushMessageAsync(dtoMessage.ChatId.ToString(), createdMsg);
            return CreatedAtAction(nameof(CreateMessage), createdMsg);
        }
    }
}
