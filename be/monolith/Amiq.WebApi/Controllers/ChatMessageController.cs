using Amiq.Business.Chat;
using Amiq.Business.Utils;
using Amiq.Contracts;
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
        private BlChatMessage _bsChatMessage = new BlChatMessage();
        private ISignalRChatService _signalRChatService;

        public ChatMessageController(ISignalRChatService signalRChatService)
        {
            _signalRChatService = signalRChatService;
        }

        [HttpGet("list-by-chat")]
        [Produces(typeof(DtoListResponseOf<DtoChatMessage>))]
        public async Task<IActionResult> GetChatMessagesByChatId([FromQuery] Guid chatId, [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var res = await _bsChatMessage.GetChatMessagesAsync(JwtStoredUserInfo.UserId, chatId, dtoPaginatedRequest);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessageById([FromQuery] DtoDeleteChatMessageRequest dtoDeleteChatMessageRequest)
        {
            dtoDeleteChatMessageRequest.IssuerId = JwtStoredUserInfo.UserId;
            var result = await _bsChatMessage.DeleteMessageAsync(dtoDeleteChatMessageRequest);
            await _signalRChatService.DeleteMessageAsync(dtoDeleteChatMessageRequest.ChatId.ToString(), dtoDeleteChatMessageRequest.MessageId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromQuery] Guid chatId, [FromBody] DtoChatMessageCreation dtoMessage)
        {
            try
            {
                var createdMsg = await _bsChatMessage.CreateChatMessageAsync(dtoMessage);
                await _signalRChatService.PushMessageAsync(dtoMessage.ChatId.ToString(), createdMsg);
                return CreatedAtAction(nameof(CreateMessage), createdMsg);
            } catch (BsRuleIsBrokenException bsException)
            {
                return UnprocessableEntity(bsException.Message);
            }
        }

        [HttpDelete("list")]
        public async Task<IActionResult> DeleteMessages([FromBody] DtoDeleteChatMessagesRequest dtoDeleteChatMessagesRequest)
        {
            if (dtoDeleteChatMessagesRequest.MessageIds.Count > 5) {
                return BadRequest();
            }
            try
            {
                var res = await _bsChatMessage.DeleteMessagesAsync(JwtStoredUserInfo.UserId, dtoDeleteChatMessagesRequest.MessageIds);
                return Ok(res);
            } catch (BsRuleIsBrokenException bsException)
            {
                return UnprocessableEntity(bsException.Message);
            }
        }
    }
}
