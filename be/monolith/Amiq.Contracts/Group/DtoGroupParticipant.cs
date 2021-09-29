using Amiq.Contracts.User;

namespace Amiq.Contracts.Group
{
    public class DtoGroupParticipant
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        //public DtoUserInfo UserInfo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AvatarPath { get; set; }
    }
}
