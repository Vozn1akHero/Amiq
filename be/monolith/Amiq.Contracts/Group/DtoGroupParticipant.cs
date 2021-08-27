using Amiq.Contracts.User;

namespace Amiq.Contracts.Group
{
    public class DtoGroupParticipant
    {
        public int GroupId { get; set; }
        public DtoUserInfo UserInfo { get; set; }
    }
}
