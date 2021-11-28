using Amiq.Contracts.Friendship;
using Amiq.DataAccessLayer.Models.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Mapping
{
    public class FriendshipMapping : Profile
    {
        public FriendshipMapping()
        {
            CreateMap<FriendRequest, DtoFriendRequest>()
                .ForMember(e => e.Receiver, d => d.MapFrom(g => g.Receiver))
                .ForMember(e => e.Creator, d => d.MapFrom(g => g.Issuer));
        }
    }
}
