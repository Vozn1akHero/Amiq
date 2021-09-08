using Amiq.Contracts.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.WebApi.SignalR
{
    public class SignalRChatService : ISignalRChatService
    {
        private readonly IHubContext<ChatHub> _chatHub;

        public SignalRChatService(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }

        public async Task DeleteMessageAsync(string group, Guid messageId, CancellationToken cancellation = default)
        {
            await _chatHub.Clients.Group(group).SendAsync(EnSignalRChatEvents.DeleteMessage, messageId, cancellation);
        }

        public async Task PushMessageAsync(string group, DtoChatMessage dtoChatMessage, CancellationToken cancellation = default)
        {
            await _chatHub.Clients.Group(group).SendAsync(EnSignalRChatEvents.PushMessage, dtoChatMessage, cancellation);
        }
    }
}
