using AmicaPlus.Base;
using AmicaPlus.Contracts.Chat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Controllers
{
    public class ChatController : AmicaPlusBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetMessagesByUserId(int userId)
        {
            return await Task.FromResult(Ok());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(DtoChatMessage message)
        {
            return await Task.FromResult(Ok());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessageById(Guid messageId)
        {
            return await Task.FromResult(Ok());
        }
    }
}
