using AmicaPlus.WebApi.Base;
using AmicaPlus.Contracts.Chat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmicaPlus.Mapping;

namespace AmicaPlus.WebApi.Controllers
{
    public class ChatController : AmicaPlusBaseController
    {

        [HttpGet("message")]
        public async Task<IActionResult> GetMessagesByUserId([FromQuery] int userId)
        {
            return await Task.FromResult(Ok());
        }

        [HttpPost("message")]
        public async Task<IActionResult> CreateMessage(DtoChatMessage dtoMessage)
        {
            var rsMessage = APAutoMapper.Instance.Map<DtoChatMessage>(dtoMessage);
            return await Task.FromResult(Ok());
        }

        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessageById(Guid messageId)
        {
            return await Task.FromResult(Ok());
        }
    }
}
