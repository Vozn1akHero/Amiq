using AmicaPlus.ResultSets.Auth;

namespace AmicaPlus.ResultSets.Group
{
    public class RsGroupParticipant
    {
        public int GroupId { get; set; }
        public RsUserInfo UserInfo { get; set; }
    }
}