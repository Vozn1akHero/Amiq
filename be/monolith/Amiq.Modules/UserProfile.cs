using Amiq.Contracts.Core;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;

namespace Amiq.Mapping
{
    public class UserProfile : APProfile
    {
        public UserProfile(){
            CreateTwoWayMap<User, DtoUserInfo>();
            CreateMap<User, DtoExtendedUserInfo>();
            CreateMap<TextBlock, DtoUserDescriptionBlock>();
            CreateMap<TextBlock, DtoDescriptionBlock>();
            CreateMap<DtoUserInfo, DtoExtendedUserInfo>();
        }
    }
}
