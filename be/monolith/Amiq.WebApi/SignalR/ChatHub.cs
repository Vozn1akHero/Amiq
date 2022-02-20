using Amiq.BusinessLayer.Chat;
using Amiq.Contracts.Auth;
using Amiq.Contracts.Chat;
using Amiq.DataAccessLayer.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Amiq.WebApi.SignalR
{
    public class ChatHub : Hub
    {
        private DaoChat _daoChat = new DaoChat();

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task JoinGroupAsync(string groupName)
        {
            int userId = (Context.GetHttpContext().Items["user"] as DtoJwtStoredUserInfo).UserId;
            Guid chatId = Guid.Parse(groupName);

            if (await _daoChat.IssuerBelongsToChat(userId, chatId))
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroupAsync(string groupName)
        {
            int userId = (Context.GetHttpContext().Items["user"] as DtoJwtStoredUserInfo).UserId;
            Guid chatId = Guid.Parse(groupName);

            if (await _daoChat.IssuerBelongsToChat(userId, chatId))
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("SendMessageAsync", message);
        }

        public async Task PushMessageAsync(string group, DtoChatMessage dtoChatMessage)
        {
            await Clients.Group(group).SendAsync(EnSignalRChatEvents.PushMessage, dtoChatMessage);
        }

        public async Task DeleteMessageAsync(string group, Guid messageId)
        {
            await Clients.Group(group).SendAsync(EnSignalRChatEvents.DeleteMessage, messageId);
        }
    }
}
