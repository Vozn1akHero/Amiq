using Amiq.Contracts.Chat;
using Amiq.DataAccessLayer.Models.Models;
using AutoMapper;
using System;

namespace Amiq.Mapping.Chat
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Amiq.DataAccessLayer.Models.Models.Chat, DtoChatPreview>();
            CreateMap<Message, DtoChatMessage>();
            CreateMap<DtoChatMessage, Message>();
            CreateMap<Message, DtoChatPreview>();
            CreateMap<DtoChatMessageCreation, Message>();
        }
    }
}
