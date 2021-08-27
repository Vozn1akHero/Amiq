using AmicaPlus.Contracts.Group;
using AmicaPlus.DataAccess.Models.Models;
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
            CreateTwoWayMap<DtoMinifiedGroupParticipant, DtoMinifiedGroupParticipant>();
            CreateMap<GroupParticipant, DtoGroupParticipant>().ForMember(s => s.UserInfo, d => d.MapFrom(s=>s.User));
            CreateMap<DtoGroupParticipant, DtoGroupParticipant>();
        }
    }
}
