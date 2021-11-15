using Amiq.Contracts.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Amiq.WebApi.SignalR
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("OnConnectedAsync", "Połączono z grupą");
            await base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("SendMessageAsync", message);
        }

        public async Task JoinGroupAsync(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroupAsync(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
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
