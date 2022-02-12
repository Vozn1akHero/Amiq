using Amiq.Services.Group.BusinessLayer.Base;
using Amiq.Services.Group.DataAccessLayer;

namespace Amiq.Services.Group.BusinessLayer.Rules
{
    public class ActionCanOnlyBePerformedByGroupParticipant : IBsRule
    {
        private DaoGroupParticipant _daGroupParticipant;
        private int _userId;
        private int _groupId;

        public ActionCanOnlyBePerformedByGroupParticipant(DaoGroupParticipant daGroupParticipant, int userId, int groupId)
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
