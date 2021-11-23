using Amiq.Contracts.Group;
using Amiq.DataAccess.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Mapping.Group
{
    public class GroupEventProfile : APProfile
    {
        public GroupEventProfile()
        {
            CreateMap<GroupEventParticipant, DtoGroupEventParticipant>();
            CreateMap<GroupEvent, DtoGroupEvent>()
                .ForPath(e=>e.GroupEventParticipantsCount, d=>d.MapFrom(g=>g.GroupEventParticipants.Count));
        }
    }
}
