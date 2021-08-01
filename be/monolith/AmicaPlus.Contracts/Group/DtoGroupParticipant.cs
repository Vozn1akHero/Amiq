using AmicaPlus.Contracts.User;

namespace AmicaPlus.Contracts.Group
{
    public class DtoGroupParticipant
    {
        public int GroupId { get; set; }
        public DtoUserInfo UserInfo { get; set; }
    }
}
