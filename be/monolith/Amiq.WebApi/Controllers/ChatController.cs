using Amiq.WebApi.Base;
using Amiq.Contracts.Chat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class ChatController : AmiqBaseController
    {

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
    }
}
