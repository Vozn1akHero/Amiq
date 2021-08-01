using AutoMapper;
using System;
using AmicaPlus.Contracts.Chat;
using AmicaPlus.ResultSets.Group;
using AmicaPlus.DataAccess.Models.Models;

namespace AmicaPlus.Mapping
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupParticipant, RsGroupParticipant>();
            
        }
    }
}
