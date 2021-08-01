using AmicaPlus.Contracts.Group;
using AmicaPlus.DataAccess.Models.Models;
using AmicaPlus.ResultSets.Group;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Mapping.Group
{
    public class GroupParticipantProfile : APProfile
    {
        public GroupParticipantProfile()
        {
            CreateTwoWayMap<RsMinifiedGroupParticipant, DtoMinifiedGroupParticipant>();
            CreateMap<GroupParticipant, RsGroupParticipant>().ForMember(s => s.UserInfo, d => d.MapFrom(s=>s.User));
            CreateMap<RsGroupParticipant, DtoGroupParticipant>();
        }
    }
}
