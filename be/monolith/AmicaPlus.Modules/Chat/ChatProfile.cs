using AutoMapper;
using System;
using AmicaPlus.Contracts.Chat;

namespace AmicaPlus.Mapping
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<DtoChatMessage, RsChatMessage>();
        }
    }
}
