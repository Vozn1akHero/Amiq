using Amiq.Contracts.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.WebApi.SignalR
{
    public interface ISignalRChatService
    {
        Task PushMessageAsync(string group, DtoChatMessage dtoChatMessage, CancellationToken cancellation = default);
        Task DeleteMessageAsync(string group, Guid messageId, CancellationToken cancellation = default);
    }
}
