using Amiq.Contracts.Chat;
using Amiq.DataAccess.Models.Models;
using AutoMapper;
using System;

namespace Amiq.Mapping
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<User, DtoChatInterlocutor>();
            CreateMap<Message, DtoChatMessage>();
            CreateMap<DtoChatMessage, Message>();
        }
    }
}
