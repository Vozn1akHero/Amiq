using Amiq.Contracts.Group;
using Amiq.DataAccess.Models.Models;
using AutoMapper;

namespace Amiq.Mapping
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<DataAccess.Models.Models.Group, DtoGroup>();
        }
    }
}
