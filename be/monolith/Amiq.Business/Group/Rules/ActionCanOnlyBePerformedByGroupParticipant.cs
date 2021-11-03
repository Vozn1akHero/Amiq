using Amiq.Business.Utils;
using Amiq.DataAccess.Group;

namespace Amiq.Business.Group.Rules
{
    public class ActionCanOnlyBePerformedByGroupParticipant : IBsRule
    {
        private DaGroupParticipant _daGroupParticipant;
        private int _userId;
        private int _groupId;

        public ActionCanOnlyBePerformedByGroupParticipant(DaGroupParticipant daGroupParticipant, int userId, int groupId)
        {
            _daGroupParticipant = daGroupParticipant;
            _userId = userId;
            _groupId = groupId;
        }

        public string ErrorContent => "Użytkownik nie jest członkiem grupy";

        public bool CheckBsRule()
        {
            var entity = _daGroupParticipant.GetGroupParticipant(_userId, _groupId);
            return entity != null;
        }
    }
}
