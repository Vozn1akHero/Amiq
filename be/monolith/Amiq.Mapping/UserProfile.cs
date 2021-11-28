using Amiq.Contracts.Core;
using Amiq.Contracts.User;
using Amiq.DataAccessLayer.Models.Models;

namespace Amiq.Mapping
{
    public class UserProfile : APProfile
    {
        public UserProfile(){
            CreateTwoWayMap<User, DtoUserInfo>();
            CreateTwoWayMap<User, DtoBasicUserInfo>();
            CreateMap<User, DtoExtendedUserInfo>();
            CreateMap<TextBlock, DtoUserDescriptionBlock>();
            CreateMap<TextBlock, DtoDescriptionBlock>();
            CreateMap<DtoUserInfo, DtoExtendedUserInfo>();
        }
    }
}
